#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Create a GitHub work item (feature / task) in the assimalign/ograph Project #8,
    following the O01.01.NN WBS scheme, native parent/sub-issue links, and the
    Summary / Acceptance Criteria body template — classifying scope creep and reusing
    existing items instead of creating duplicates.

.DESCRIPTION
    The reliable, multi-step intake path used by the `ograph-work-items` skill:

      1. Resolve the PARENT issue (from -Parent, or inferred from the current branch's WBS code).
      2. Validate placement (parent must be an area epic or a feature) and compute the next free
         child WBS code (max existing direct child + 1, across OPEN and CLOSED siblings).
      3. Look for EXISTING items that may already cover the request; block on a likely duplicate
         unless -Force (reuse beats re-filing).
      4. Create the issue ([<wbs>] <title> + templated body) with `gh issue create`.
      5. Add it to Project #8 and set Status / Priority / Wave / Kind / Area / Origin fields.
      6. Link it to its parent as a native GitHub sub-issue (addSubIssue mutation, error-checked).
      7. Record it (with its Origin) in a per-branch manifest so the eventual PR can close every
         captured item and report how much of the work was scope creep.

    Every network call goes through a rate-limit-aware wrapper that backs off (honoring Retry-After
    and the rate_limit reset, else exponential backoff with jitter) and retries.

    "Origin" classifies how the item was born so scope creep becomes measurable:
      Planned           - filed deliberately under an explicit -Parent.
      DiscoveredTask    - in-feature creep: a separable task discovered while building the feature.
      DiscoveredFeature - escaped-feature creep: out-of-feature work that became a sibling feature.
    Discovered items also get the `scope-creep` label and a body provenance stamp. Features get the
    `enhancement` label automatically.

    Single-select fields are resolved by NAME at runtime (never hardcoded ids), so the script keeps
    working if the project schema changes. See ../reference/project-schema.md for the manual recipe.

.EXAMPLE
    # Before creating, see whether anything already covers the work:
    ./New-OGraphWorkItem.ps1 -Search "query method body binding"

.EXAMPLE
    # Scope-creep TASK discovered on feature branch feature/O01.01.05.01-query-method.
    ./New-OGraphWorkItem.ps1 -As task -Title "Validate query Content-Type against RFC 10008 media-type rules" `
        -Summary "Hardening discovered while wiring the QUERY method binding; outside the original feature scope."

.EXAMPLE
    # Out-of-feature scope -> sibling FEATURE under the area epic; Origin=DiscoveredFeature.
    ./New-OGraphWorkItem.ps1 -As feature -Title "Implement ETag validators and conditional QUERY caching" -Wave W04

.EXAMPLE
    # Assemble the grouped "Closes #N" block (planned vs discovered) for the current branch's PR body.
    ./New-OGraphWorkItem.ps1 -EmitClosesBlock
#>
[CmdletBinding(DefaultParameterSetName = 'Create')]
param(
    [Parameter(ParameterSetName = 'Create', Mandatory = $true)]
    [string] $Title,

    # Where the item nests when -Parent is not given:
    #   task    -> child of the FEATURE named by the current branch's WBS code
    #   feature -> child of the AREA epic (branch WBS with its last segment dropped)
    [Parameter(ParameterSetName = 'Create')]
    [ValidateSet('task', 'feature')]
    [string] $As = 'task',

    # Explicit parent: an issue number (e.g. 314) or a WBS code (e.g. O01.01.05). Overrides -As inference.
    [Parameter(ParameterSetName = 'Create')]
    [string] $Parent,

    # How the item was born. Inferred when omitted; pass to override.
    [Parameter(ParameterSetName = 'Create')]
    [ValidateSet('Planned', 'DiscoveredTask', 'DiscoveredFeature')]
    [string] $Origin,

    [Parameter(ParameterSetName = 'Create')]
    [string] $Summary,

    [Parameter(ParameterSetName = 'Create')]
    [string[]] $Acceptance,

    [Parameter(ParameterSetName = 'Create')]
    [string[]] $Standards,

    # Provide a complete body instead of the generated template.
    [Parameter(ParameterSetName = 'Create')]
    [string] $BodyFile,

    [Parameter(ParameterSetName = 'Create')]
    [ValidateSet('Backlog', 'Ready', 'In progress', 'In review', 'Done')]
    [string] $Status = 'Backlog',

    [Parameter(ParameterSetName = 'Create')]
    [ValidateSet('P001', 'P002', 'P003', 'P004', 'P005', 'P006', 'P007')]
    [string] $Priority,

    [Parameter(ParameterSetName = 'Create')]
    [ValidateSet('W01', 'W02', 'W03', 'W04', 'W05', 'W06')]
    [string] $Wave,

    # GitHub labels to apply (comma list or repeated). 'scope-creep' is added automatically for discovered
    # items; 'enhancement' is added automatically for features.
    [Parameter(ParameterSetName = 'Create')]
    [string[]] $Label,

    # Create even when a likely-duplicate existing item is found.
    [Parameter(ParameterSetName = 'Create')]
    [switch] $Force,

    [Parameter(ParameterSetName = 'Create')]
    [switch] $DryRun,

    # Read-only: list existing work items whose titles look similar to the given text, then exit.
    [Parameter(ParameterSetName = 'Search', Mandatory = $true)]
    [string] $Search,

    # Print the grouped "Closes #N" block for every work item captured on the current branch, then exit.
    [Parameter(ParameterSetName = 'Closes', Mandatory = $true)]
    [switch] $EmitClosesBlock
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

# --- Constants -------------------------------------------------------------------------------
$Owner       = 'assimalign'
$Repo        = 'assimalign/ograph'
$ProjectNum  = 8
$IssueLimit  = 5000
$script:AllIssuesCache = $null
$StopWords = @('add','the','and','for','with','to','of','a','an','is','are','be','via','use','new',
               'implement','implementation','support','enable','provide','create','make','into','over')

# --- Rate-limit-aware gh wrapper -------------------------------------------------------------

function Test-RateLimited {
    param([string] $Text)
    return [bool]($Text -match '(?i)(api rate limit exceeded|secondary rate limit|exceeded a secondary|abuse detection|was submitted too quickly|RATE_LIMITED|retry[- ]after|\b429\b)')
}

function Get-EpochNow { return [int][double]::Parse((Get-Date -UFormat %s)) }

function Get-BackoffSeconds {
    param([int] $Attempt, [string] $Text)
    # 1. Honor an explicit Retry-After (typical for secondary limits)
    $m = [regex]::Match($Text, '(?i)retry[- ]after[:\s]+(\d+)')
    if ($m.Success) { return [Math]::Min([int]$m.Groups[1].Value + 1, 300) }
    # 2. For a primary limit, sleep until the soonest exhausted resource resets
    if ($Text -match '(?i)api rate limit exceeded') {
        try {
            $rl = & gh api rate_limit 2>$null | ConvertFrom-Json
            $resets = @()
            foreach ($p in $rl.resources.PSObject.Properties) {
                if ($p.Value.remaining -le 0) { $resets += [int]$p.Value.reset }
            }
            if ($resets.Count -gt 0) {
                $wait = (($resets | Measure-Object -Minimum).Minimum) - (Get-EpochNow)
                if ($wait -gt 0) { return [Math]::Min($wait + 2, 300) }
            }
        } catch { }
    }
    # 3. Exponential backoff with jitter
    $base = [Math]::Min(8 * [Math]::Pow(2, $Attempt - 1), 180)
    return [int]($base + (Get-Random -Minimum 0 -Maximum 5))
}

# Run gh with retry on rate limits. -AllowFailure returns $null instead of throwing on terminal failure.
# -GraphQL also treats an exit-0 response carrying a rate-limited `errors` array as retryable.
function Invoke-GhRetry {
    param(
        [Parameter(Mandatory = $true)][string[]] $GhArgs,
        [switch] $AllowFailure,
        [switch] $GraphQL,
        [int] $MaxAttempts = 6
    )
    for ($attempt = 1; ; $attempt++) {
        $out  = & gh @GhArgs 2>&1
        $code = $LASTEXITCODE
        $text = ($out | Out-String)
        # A GraphQL call exits 0 even when its body carries an `errors` array, so treat that as a failure.
        $gqlError  = ($code -eq 0 -and $GraphQL -and $text -match '"errors"')
        $retryable = if ($code -ne 0) { Test-RateLimited $text }
                     elseif ($gqlError) { Test-RateLimited $text }
                     else { $false }
        if ($code -eq 0 -and -not $gqlError) { return $out }   # clean success
        if ($retryable -and $attempt -lt $MaxAttempts) {
            $wait = Get-BackoffSeconds -Attempt $attempt -Text $text
            Write-Warning "GitHub rate limit hit; backing off $([int]$wait)s then retrying (attempt $attempt/$MaxAttempts)."
            Start-Sleep -Seconds $wait
            continue
        }
        # terminal: a non-zero exit, or a GraphQL error body that isn't a rate limit
        if ($AllowFailure) { return $null }
        throw "gh $($GhArgs -join ' ') failed (attempt $attempt):`n$text"
    }
}

# DryRun-aware wrapper for MUTATING gh calls.
function Invoke-Gh {
    param([Parameter(ValueFromRemainingArguments = $true)][string[]] $GhArgs)
    if ($DryRun) { Write-Host "DRYRUN gh $($GhArgs -join ' ')" -ForegroundColor DarkYellow; return $null }
    return Invoke-GhRetry -GhArgs $GhArgs
}

# --- Issue / WBS helpers ---------------------------------------------------------------------

function Get-WbsTokenCount { param([string] $Wbs) return ($Wbs -split '\.').Count }

function Get-BranchWbs {
    $branch = (& git rev-parse --abbrev-ref HEAD 2>$null)
    if (-not $branch) { return $null }
    $m = [regex]::Match($branch, 'O\d{2}(?:\.\d{2})+')
    if ($m.Success) { return $m.Value }
    return $null
}

# Absolute, cwd-independent, per-worktree manifest path under the git dir.
function Get-ManifestPath {
    $gitDir = (& git rev-parse --absolute-git-dir 2>$null)
    if (-not $gitDir) { return $null }
    $dir = Join-Path $gitDir 'ograph'
    if (-not (Test-Path $dir)) { New-Item -ItemType Directory -Force -Path $dir | Out-Null }
    return (Join-Path $dir 'workitems.tsv')
}

# Fetch every issue (open + closed) once and cache it. We filter client-side because GitHub's
# `--search` tokenizes '[', ']' and '.' unreliably and silently drops dotted-code siblings.
function Get-AllIssues {
    if ($null -ne $script:AllIssuesCache) { return $script:AllIssuesCache }
    $json = Invoke-GhRetry -GhArgs @('issue','list','--repo',$Repo,'--state','all','--limit',"$IssueLimit",'--json','number,title,url,id,state') | ConvertFrom-Json
    $arr = @($json)
    if ($arr.Count -ge $IssueLimit) {
        throw "Issue enumeration hit the --limit ($IssueLimit) cap; next-WBS computation may miss siblings and could produce a DUPLICATE code. Raise `$IssueLimit or page the cursor before continuing."
    }
    $script:AllIssuesCache = $arr
    return $script:AllIssuesCache
}

function Get-IssueByWbs {
    param([string] $Wbs)
    $rx = '^\[' + [regex]::Escape($Wbs) + '\]'
    return (Get-AllIssues) | Where-Object { $_.title -match $rx } | Select-Object -First 1
}

function Get-IssueByNumber {
    param([int] $Number)
    $out = Invoke-GhRetry -GhArgs @('issue','view',"$Number",'--repo',$Repo,'--json','number,title,url,id,state') -AllowFailure
    if (-not $out) { return $null }
    return $out | ConvertFrom-Json
}

# Largest existing DIRECT child index under a parent WBS (matches "[parent.NN]" only, not deeper),
# across open AND closed issues so numbers are never reused. Returns the next code.
function Get-NextChildWbs {
    param([string] $ParentWbs)
    $rx = '^\[' + [regex]::Escape($ParentWbs) + '\.(\d{2})\]'
    $max = 0
    foreach ($i in (Get-AllIssues)) {
        $m = [regex]::Match($i.title, $rx)
        if ($m.Success) { $n = [int]$m.Groups[1].Value; if ($n -gt $max) { $max = $n } }
    }
    return ('{0}.{1:D2}' -f $ParentWbs, ($max + 1))
}

# --- Existing-item discovery (reuse before creating) -----------------------------------------

function Get-TitleTokens {
    param([string] $Text)
    if (-not $Text) { return @() }
    return @(($Text.ToLowerInvariant() -split '[^a-z0-9]+') |
        Where-Object { $_.Length -ge 3 -and $StopWords -notcontains $_ } | Select-Object -Unique)
}

# Rank existing issues by significant-word overlap with the given title text (the [WBS] prefix is
# stripped from candidates before tokenizing). Returns up to -Top scored candidates.
function Find-SimilarIssues {
    param([string] $TitleText, [int] $Top = 6)
    $want = @(Get-TitleTokens $TitleText)
    if ($want.Count -eq 0) { return @() }
    $scored = foreach ($i in (Get-AllIssues)) {
        $bare = ($i.title -replace '^\[[^\]]+\]\s*', '')
        $have = @(Get-TitleTokens $bare)
        if ($have.Count -eq 0) { continue }
        $shared = @($want | Where-Object { $have -contains $_ })
        if ($shared.Count -eq 0) { continue }
        $union = @($want + $have | Select-Object -Unique).Count
        [pscustomobject]@{
            Number  = $i.number
            Title   = $i.title
            State   = $i.state
            Shared  = $shared.Count
            Jaccard = [Math]::Round($shared.Count / $union, 2)
            Terms   = ($shared -join ', ')
        }
    }
    return @($scored | Sort-Object -Property @{Expression='Shared';Descending=$true}, @{Expression='Jaccard';Descending=$true} | Select-Object -First $Top)
}

# --- Project field helpers -------------------------------------------------------------------

function Get-FieldMap {
    $fields = Invoke-GhRetry -GhArgs @('project','field-list',"$ProjectNum",'--owner',$Owner,'--format','json') | ConvertFrom-Json
    $map = @{}
    foreach ($f in $fields.fields) {
        $opts = @{}
        if ($f.PSObject.Properties.Name -contains 'options' -and $f.options) {
            foreach ($o in $f.options) { $opts[$o.name] = $o.id }
        }
        $map[$f.name] = @{ id = $f.id; options = $opts }
    }
    return $map
}

function Set-ProjectField {
    param($FieldMap, [string] $ProjectId, [string] $ItemId, [string] $FieldName, [string] $OptionName)
    if (-not $OptionName) { return }
    $field = $FieldMap[$FieldName]
    if (-not $field) { Write-Host "  ($FieldName not a project field; skipped)" -ForegroundColor DarkGray; return }
    $optId = $field.options[$OptionName]
    if (-not $optId) { Write-Warning "Option '$OptionName' not found on field '$FieldName'; skipping."; return }
    Invoke-Gh project item-edit --id $ItemId --project-id $ProjectId `
        --field-id $field.id --single-select-option-id $optId | Out-Null
    Write-Host "  set $FieldName = $OptionName" -ForegroundColor DarkGray
}

function New-BodyTemplate {
    param([string] $Sum, [string[]] $Acc, [string[]] $Std)
    $sb = [System.Text.StringBuilder]::new()
    [void]$sb.AppendLine('## Summary')
    if ($Sum) { [void]$sb.AppendLine($Sum) }
    else { [void]$sb.AppendLine('- <one or two sentences: what and why; note this was captured as out-of-scope work>') }
    [void]$sb.AppendLine('')
    [void]$sb.AppendLine('## Acceptance Criteria')
    if ($Acc) { foreach ($a in $Acc) { [void]$sb.AppendLine("- $a") } }
    else {
        [void]$sb.AppendLine('- <observable, testable outcome>')
        [void]$sb.AppendLine('- Tests cover the new behavior.')
        [void]$sb.AppendLine('- The implementation remains NativeAOT-safe and trimming-safe.')
    }
    [void]$sb.AppendLine('')
    [void]$sb.AppendLine('### Standards and Compliance')
    if ($Std) { foreach ($s in $Std) { [void]$sb.AppendLine("- $s") } }
    else { [void]$sb.AppendLine('- Conforms to the OGraph specification section it implements (cite it), or note that it is a runtime-contract concern; remains NativeAOT-safe and trimming-safe.') }
    return $sb.ToString()
}

# --- Search mode -----------------------------------------------------------------------------

if ($PSCmdlet.ParameterSetName -eq 'Search') {
    $cands = @(Find-SimilarIssues -TitleText $Search -Top 10)
    if ($cands.Count -eq 0) {
        Write-Host "No similar existing work items found for: $Search" -ForegroundColor Yellow
        return
    }
    Write-Host "Existing work items that may already cover: $Search" -ForegroundColor Cyan
    foreach ($c in $cands) {
        Write-Host ("  #{0} [{1}] shared={2} jaccard={3}  {4}" -f $c.Number, $c.State, $c.Shared, $c.Jaccard, $c.Title)
    }
    Write-Host "Reuse one of these instead of creating a duplicate where it fits." -ForegroundColor DarkGray
    return
}

# --- Closes-block mode -----------------------------------------------------------------------

if ($PSCmdlet.ParameterSetName -eq 'Closes') {
    $branch = (& git rev-parse --abbrev-ref HEAD 2>$null)
    $manifest = Get-ManifestPath
    if (-not $manifest -or -not (Test-Path $manifest)) {
        Write-Host "# No work items recorded for branch '$branch'." -ForegroundColor Yellow
        return
    }
    $planned = @(); $discovered = @()
    foreach ($l in (Get-Content $manifest)) {
        if (-not $l) { continue }
        $parts = $l -split "`t"
        if ($parts.Count -lt 2) { continue }          # skip malformed rows rather than throwing
        if ($parts[0] -ne $branch) { continue }
        $num = $parts[1]
        if ($num -notmatch '^\d+$') { continue }       # second field must be an issue number
        $origin = if ($parts.Count -ge 4) { $parts[3] } else { 'Planned' }
        if ($origin -like 'Discovered*') { $discovered += $num } else { $planned += $num }
    }
    if ($planned.Count -eq 0 -and $discovered.Count -eq 0) {
        Write-Host "# No work items recorded for branch '$branch'." -ForegroundColor Yellow
        return
    }
    $total = $planned.Count + $discovered.Count
    Write-Output "## Work items resolved by this PR"
    foreach ($n in $planned) { Write-Output "Closes #$n" }
    if ($discovered.Count -gt 0) {
        Write-Output ""
        Write-Output "### Discovered (out-of-scope) work"
        foreach ($n in $discovered) { Write-Output "Closes #$n" }
        Write-Output ""
        Write-Output "<!-- creep: $($discovered.Count) of $total items were discovered out-of-scope -->"
    }
    return
}

# --- Create mode -----------------------------------------------------------------------------

# 1. Resolve parent issue + WBS
$parentIssue = $null
$parentWbs   = $null
$inferred    = $false
$branchWbs   = Get-BranchWbs

if ($Parent) {
    if ($Parent -match '^\d+$') {
        $parentIssue = Get-IssueByNumber -Number ([int]$Parent)
        if (-not $parentIssue) { throw "Parent issue #$Parent not found." }
        $m = [regex]::Match($parentIssue.title, '^\[(O\d{2}(?:\.\d{2})+)\]')
        if (-not $m.Success) { throw "Parent #$Parent title has no [WBS] prefix: '$($parentIssue.title)'." }
        $parentWbs = $m.Groups[1].Value
    }
    else {
        $parentWbs = $Parent
        $parentIssue = Get-IssueByWbs -Wbs $parentWbs
        if (-not $parentIssue) { throw "No issue found with title prefix [$parentWbs]." }
    }
}
else {
    $inferred = $true
    if (-not $branchWbs) { throw "Could not infer a WBS code from the current branch. Pass -Parent <issue#|WBS> explicitly." }
    if ((Get-WbsTokenCount $branchWbs) -ne 4) {
        throw "Branch WBS [$branchWbs] is not a feature (4 segments); cannot infer placement. Pass -Parent explicitly."
    }
    if ($As -eq 'feature') { $parentWbs = ($branchWbs -replace '\.\d{2}$', '') }  # sibling feature -> area epic
    else { $parentWbs = $branchWbs }                                              # task -> the feature itself
    $parentIssue = Get-IssueByWbs -Wbs $parentWbs
    if (-not $parentIssue) { throw "No parent issue found for WBS [$parentWbs] (inferred from branch). Pass -Parent explicitly." }
}

# Validate placement: parent must be an area epic (3 seg -> child feature) or feature (4 seg -> child task).
$parentTokens = Get-WbsTokenCount $parentWbs
switch ($parentTokens) {
    3       { $level = 'feature' }
    4       { $level = 'task' }
    default { throw "Parent [$parentWbs] is neither an area epic (3 seg) nor a feature (4 seg); cannot place a child under it." }
}
if ($inferred -and $level -ne $As) {
    throw "Inferred parent [$parentWbs] yields a '$level' but -As '$As' was requested. Pass -Parent explicitly to override."
}

# Origin classification (scope-creep provenance)
if (-not $Origin) {
    if ($inferred) { $Origin = if ($level -eq 'feature') { 'DiscoveredFeature' } else { 'DiscoveredTask' } }
    else { $Origin = 'Planned' }
}

Write-Host "Parent: #$($parentIssue.number) $($parentIssue.title)" -ForegroundColor Green

# 2. Next child WBS code + derived Kind/Area
$newWbs = Get-NextChildWbs -ParentWbs $parentWbs
$fullTitle = "[$newWbs] $Title"
$kindOpt = if ($level -eq 'feature') { 'Feature' } else { 'Task' }
# Area epic code = the first 3 segments of the (already 3-or-4-segment-validated) parent WBS.
$areaCode = ((($parentWbs -split '\.') | Select-Object -First 3) -join '.')
$areaEpic = Get-IssueByWbs -Wbs $areaCode
$areaName = $null
if ($areaEpic) {
    $am = [regex]::Match($areaEpic.title, 'OGraph - (.+)$')
    if ($am.Success) { $areaName = $am.Groups[1].Value.Trim() }
}
Write-Host "New $level ($Origin): $fullTitle" -ForegroundColor Green

# 3. Reuse before creating: look for an existing item that may already cover this
$similar = @(Find-SimilarIssues -TitleText $Title)
$strong  = @($similar | Where-Object { $_.State -eq 'OPEN' -and ($_.Jaccard -ge 0.5 -or $_.Shared -ge 3) })
if ($strong.Count -gt 0 -and -not $Force) {
    Write-Warning "A likely-duplicate existing item was found for '$Title':"
    foreach ($c in $strong) { Write-Host "  #$($c.Number) [$($c.State)] $($c.Title)  (shared=$($c.Shared), jaccard=$($c.Jaccard); terms: $($c.Terms))" -ForegroundColor Yellow }
    Write-Host "Reuse one of the above (add work to it), or re-run with -Force to create a new item anyway." -ForegroundColor Yellow
    return
}
elseif ($similar.Count -gt 0) {
    Write-Host "Similar existing items (not blocking — confirm this is genuinely new):" -ForegroundColor DarkGray
    foreach ($c in ($similar | Select-Object -First 3)) { Write-Host "  #$($c.Number) [$($c.State)] $($c.Title)" -ForegroundColor DarkGray }
}

# 4. Body (+ provenance stamp for discovered work, which can lose the back-pointer to the spawning feature)
if ($BodyFile) {
    if (-not (Test-Path $BodyFile)) { throw "BodyFile not found: $BodyFile" }
    $body = Get-Content $BodyFile -Raw
}
else {
    $body = New-BodyTemplate -Sum $Summary -Acc $Acceptance -Std $Standards
}
if ($Origin -like 'Discovered*' -and $branchWbs) {
    $feat = Get-IssueByWbs -Wbs $branchWbs
    $featRef = if ($feat) { "[$branchWbs] (#$($feat.number))" } else { "[$branchWbs]" }
    $body = "> Discovered while implementing $featRef.`n`n" + $body
}

# Discovered work is labeled scope-creep so it is countable from the issues CLI; features carry enhancement.
$labels = @($Label | Where-Object { $_ })
if ($Origin -like 'Discovered*' -and $labels -notcontains 'scope-creep') { $labels += 'scope-creep' }
if ($level -eq 'feature' -and $labels -notcontains 'enhancement') { $labels += 'enhancement' }

$tmp = New-TemporaryFile
Set-Content -Path $tmp -Value $body -Encoding UTF8

$createArgs = @('issue', 'create', '--repo', $Repo, '--title', $fullTitle, '--body-file', "$tmp")
foreach ($lbl in $labels) { $createArgs += @('--label', $lbl) }

if ($DryRun) {
    Write-Host "DRYRUN gh $($createArgs -join ' ')" -ForegroundColor DarkYellow
    Write-Host "DRYRUN body:`n$body" -ForegroundColor DarkGray
    Write-Host "DRYRUN gh api -X PATCH repos/$Repo/issues/<new#> -f type=$kindOpt  (stamp native issue type)" -ForegroundColor DarkYellow
    Remove-Item $tmp -Force
    $extra = @("Status=$Status", "Kind=$kindOpt", "Area=$areaName", "Origin=$Origin")
    if ($Priority) { $extra += "Priority=$Priority" }
    if ($Wave)     { $extra += "Wave=$Wave" }
    Write-Host "DRYRUN would: add to Project #$ProjectNum, set $($extra -join ', '); stamp native issue type=$kindOpt; labels [$($labels -join ', ')]; link as sub-issue of #$($parentIssue.number); record in branch manifest." -ForegroundColor DarkYellow
    return
}

# 5. Create issue
$createOut = Invoke-GhRetry -GhArgs $createArgs
Remove-Item $tmp -Force
$issueUrl = ($createOut | Select-String -Pattern 'https://github\.com/\S+/issues/\d+' | Select-Object -Last 1)
if (-not $issueUrl) { throw "Issue creation failed or no issue URL returned:`n$createOut" }
$issueUrl  = $issueUrl.Matches[0].Value
$newNumber = [int]($issueUrl -split '/')[-1]
Write-Host "Created #$newNumber -> $issueUrl" -ForegroundColor Green

# 5b. Stamp the native GitHub issue type (title badge + project group-by "Type"): feature -> Feature, task -> Task.
#     Routed through Invoke-Gh so rate-limit/DryRun handling applies. Non-fatal: the item is already
#     created and will be linked, so a failure warns rather than aborts.
try {
    Invoke-Gh api -X PATCH "repos/$Repo/issues/$newNumber" -f "type=$kindOpt" | Out-Null
    Write-Host "Set native issue type = $kindOpt" -ForegroundColor Green
} catch {
    Write-Warning "Could not set native issue type '$kindOpt' on #$newNumber; set it manually: gh api -X PATCH repos/$Repo/issues/$newNumber -f type=$kindOpt`n$_"
}

# 6. Add to project and set fields
$projectId = (Invoke-GhRetry -GhArgs @('project','view',"$ProjectNum",'--owner',$Owner,'--format','json') | ConvertFrom-Json).id
$itemId    = (Invoke-GhRetry -GhArgs @('project','item-add',"$ProjectNum",'--owner',$Owner,'--url',$issueUrl,'--format','json') | ConvertFrom-Json).id
Write-Host "Added to Project #$ProjectNum (item $itemId)" -ForegroundColor Green

$fieldMap = Get-FieldMap
Set-ProjectField -FieldMap $fieldMap -ProjectId $projectId -ItemId $itemId -FieldName 'Status'   -OptionName $Status
Set-ProjectField -FieldMap $fieldMap -ProjectId $projectId -ItemId $itemId -FieldName 'Kind'     -OptionName $kindOpt
Set-ProjectField -FieldMap $fieldMap -ProjectId $projectId -ItemId $itemId -FieldName 'Area'     -OptionName $areaName
Set-ProjectField -FieldMap $fieldMap -ProjectId $projectId -ItemId $itemId -FieldName 'Origin'   -OptionName $Origin
Set-ProjectField -FieldMap $fieldMap -ProjectId $projectId -ItemId $itemId -FieldName 'Priority' -OptionName $Priority
Set-ProjectField -FieldMap $fieldMap -ProjectId $projectId -ItemId $itemId -FieldName 'Wave'     -OptionName $Wave

# 7. Link as native sub-issue of the parent (gh exits 0 even on GraphQL errors, so inspect the body)
$parentNodeId = $parentIssue.id
$childNodeId  = (Invoke-GhRetry -GhArgs @('issue','view',"$newNumber",'--repo',$Repo,'--json','id') | ConvertFrom-Json).id
$linkOut = Invoke-GhRetry -GraphQL -AllowFailure -GhArgs @('api','graphql',
    '-f',"query=mutation(`$p:ID!,`$c:ID!){ addSubIssue(input:{issueId:`$p, subIssueId:`$c}){ subIssue { number } } }",
    '-F',"p=$parentNodeId",'-F',"c=$childNodeId")
$linkResp  = if ($linkOut) { try { $linkOut | ConvertFrom-Json } catch { $null } } else { $null }
$linkedNum = $null
if ($linkResp -and $linkResp.PSObject.Properties.Name -contains 'data' -and $linkResp.data.addSubIssue) {
    $linkedNum = $linkResp.data.addSubIssue.subIssue.number
}
if ($linkedNum) { Write-Host "Linked #$newNumber as sub-issue of #$($parentIssue.number)" -ForegroundColor Green }
else { Write-Warning ("Sub-issue link failed; set the parent of #$newNumber to #$($parentIssue.number) manually." + $(if ($linkOut) { "`n$linkOut" })) }

# 8. Record in per-branch manifest for PR close-out (sanitize so tabs/newlines can't corrupt a row)
$branch = (& git rev-parse --abbrev-ref HEAD 2>$null)
$manifest = Get-ManifestPath
if ($manifest) {
    $safeTitle = ($Title -replace "[`t`r`n]", ' ')
    Add-Content -Path $manifest -Value ("{0}`t{1}`t{2}`t{3}`t{4}" -f $branch, $newNumber, $newWbs, $Origin, $safeTitle)
    Write-Host "Recorded in branch manifest ($manifest)" -ForegroundColor DarkGray
}

Write-Host ""
Write-Host "Done [$Origin]. Add to your PR body:  Closes #$newNumber" -ForegroundColor Cyan
