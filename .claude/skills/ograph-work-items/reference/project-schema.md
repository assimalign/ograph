# OGraph GitHub Project — schema & manual recipes

Reference for the `ograph-work-items` skill. Captured from the live project on 2026-07-16 during
initial population. The helper script (`New-OGraphWorkItem.ps1`) resolves IDs **dynamically** by
name, so these are for the manual path and for understanding the model. If an ID stops working,
re-run the discovery commands at the bottom.

## Coordinates

| Thing | Value |
| --- | --- |
| Repo | `assimalign/ograph` |
| Org / owner | `assimalign` |
| Project | **#8 "OGraph"** |
| Project node id | `PVT_kwDOA9eCcc4AL09m` |

## WBS taxonomy

Work items carry their position in the title as `[<code>] <description>`. The hierarchy is held by
**native GitHub parent/sub-issue links**, and every item is added to Project #8.

| Code shape | Segments | Level | Title pattern | Example |
| --- | --- | --- | --- | --- |
| `O01.01.00` | 3 (`.00`) | Program root | `[O01.01.00] OGraph Protocol & Libraries` (#5) | — |
| `O01.01.NN` | 3 | **Area epic** | `[O01.01.NN] OGraph - <Area>` | `[O01.01.05] OGraph - Server` (#10) |
| `O01.01.NN.MM` | 4 | **Feature** | `[O01.01.NN.MM] <imperative>` | `[O01.01.05.01] Add HTTP QUERY method support…` (#43) |
| `O01.01.NN.MM.PP` | 5 | **Task** | `[O01.01.NN.MM.PP] <imperative>` | leaf (filed as work is discovered) |

`O` = OGraph program; `O01.01` = OGraph Protocol & Libraries. Area epics are titled `OGraph - <Area>`.

> The program root deliberately has **no** `OGraph - ` prefix, so the area-title regex
> `OGraph - (.+)$` never matches it. Epic-discovery searches must still exclude the `.00` root by code.

Branch convention: `feature/<wbs>-<slug>` (e.g. `feature/O01.01.05.01-query-method`). The WBS in the
branch names the **feature** currently in flight. The branch-WBS regex is `O\d{2}(?:\.\d{2})+`.

### Area epics (parents for new sibling features)

| Issue | Code | Area |
| --- | --- | --- |
| #6  | O01.01.01 | Specification |
| #7  | O01.01.02 | Gdm |
| #8  | O01.01.03 | Syntax |
| #9  | O01.01.04 | Core |
| #10 | O01.01.05 | Server |
| #11 | O01.01.06 | Client |
| #12 | O01.01.07 | ToolKit |
| #13 | O01.01.08 | Analyzers |
| #14 | O01.01.09 | Sdk |
| #15 | O01.01.10 | Cli |
| #16 | O01.01.11 | Extensions |
| #17 | O01.01.12 | Engineering |

Program root: **#5** `[O01.01.00] OGraph Protocol & Libraries`.

(Re-list with: `gh issue list --repo assimalign/ograph --state open --search '"OGraph -" in:title' --json number,title`)

## Custom fields (single-select)

The script resolves these **by name** at runtime, so the ids below are only for the manual path.

| Field | Field id | Options (name = optionId) |
| --- | --- | --- |
| **Status** | `PVTSSF_lADOA9eCcc4AL09mzgHixXo` | Backlog=`98cc1adc`, Ready=`f0229cca`, In progress=`30253884`, In review=`0d561d01`, Done=`46e6bcac` |
| **Kind** | `PVTSSF_lADOA9eCcc4AL09mzhYFN20` | Program=`8f49031b`, Area Epic=`390a1ee9`, Feature=`0ccdae5c`, Task=`d68430e1` |
| **Area** | `PVTSSF_lADOA9eCcc4AL09mzhYFN24` | Specification=`9f1a7b7d`, Gdm=`fad8f398`, Syntax=`87520bab`, Core=`55e3991f`, Server=`bc9222d4`, Client=`4408dcc9`, ToolKit=`c2a0dbfa`, Analyzers=`6b59d171`, Sdk=`94c91aba`, Cli=`204c0e18`, Extensions=`692faf06`, Engineering=`c9153799` |
| **Origin** | `PVTSSF_lADOA9eCcc4AL09mzhYFN28` | Planned=`9b6477d7`, DiscoveredTask=`d6dbf10e`, DiscoveredFeature=`33b23c53` |
| **Priority** | `PVTSSF_lADOA9eCcc4AL09mzhYFN3A` | P001=`053c1f17`, P002=`8c19053f`, P003=`f5679f5f`, P004=`4ce9597e`, P005=`0ae3614a`, P006=`7b59b07e`, P007=`c9968873` |
| **Wave** | `PVTSSF_lADOA9eCcc4AL09mzhYFN34` | W01=`111bd3ad`, W02=`7cb402a2`, W03=`fb8c7cb2`, W04=`d532d8b5`, W05=`8e0b01ce`, W06=`a6e996e4` |

The script sets **Status, Kind, Area, Origin** on every item it creates (plus Priority/Wave when passed).
`Kind` comes from the WBS depth (Feature/Task), `Area` from the area-epic ancestor's `OGraph - X`
title, `Origin` from the scope-creep classification. Discovered items also get the **`scope-creep`**
repo label (color `D93F0B`, "Discovered out-of-scope work"). Features additionally get the
**`enhancement`** label.

Priority `P001` (highest) → `P007` (lowest). Waves `W01` (earliest) → `W06` (latest); a feature's wave
is when it is *expected* to start. See `docs/DELIVERY_ROADMAP.md` §4 for wave exit criteria.

Also present (built-in / non-select): Title, Assignees, Labels, Linked pull requests, Milestone,
Repository, Reviewers, Parent issue, Sub-issues progress, Created, Updated, Closed.

### Counting scope creep

```bash
# By label (issues CLI):
gh issue list --repo assimalign/ograph --label scope-creep --state all --json number,title,state
# By field on the board: filter or group Project #8 by Origin (DiscoveredTask / DiscoveredFeature).
```

## Body template (de-facto standard across issues)

```markdown
## Summary
<one to three sentences: what and why. For scope-creep items, note it was discovered out of scope.>

## Acceptance Criteria
- <observable, testable outcome>
- Tests cover the new behavior.                         <!-- code features -->
- The implementation remains NativeAOT-safe and trimming-safe.   <!-- code features -->

### Standards and Compliance
- <Protocol/spec features: cite RFC 10008 / the roadmap spec feature they implement,
   e.g. "Implements roadmap S-05 / RFC 10008 §2.x". Non-protocol features: note the
   runtime-contract / build-infra / documentation concern.>
```

Every shipped library **must be NativeAOT-compatible** (roadmap D3). Spec/doc/hygiene items omit the
two AOT/test bullets; runtime-code features include them.

## Manual recipe (when not using the helper script)

```bash
REPO=assimalign/ograph ; OWNER=assimalign ; PROJ=8
PROJECT_ID=PVT_kwDOA9eCcc4AL09m

# 1. Find the parent + its node id (feature for a task, area epic for a sibling feature)
gh issue view 10 --repo $REPO --json number,title,id     # e.g. the Server epic

# 2. Find the next free child number. Do NOT use --search for the dotted code (it silently drops
#    siblings). Fetch all issues and filter on the title with gh's built-in jq (-q):
gh issue list --repo $REPO --state all --limit 5000 --json number,title \
  -q '.[] | select(.title | test("^\\[O01\\.01\\.05\\.[0-9]{2}\\]")) | .title'
#    Then take the max trailing NN across OPEN and CLOSED, add 1, zero-pad to two digits.

# 3. Create the issue (features get --label enhancement; discovered items also --label scope-creep)
URL=$(gh issue create --repo $REPO \
  --title '[O01.01.05.12] <short imperative description>' \
  --label enhancement \
  --body-file body.md)
NUM=${URL##*/}

# 4. Add to project, capture the project item id
ITEM=$(gh project item-add $PROJ --owner $OWNER --url "$URL" --format json --jq .id)

# 5. Set fields (repeat with the ids from the table above): Status, Kind, Area, Origin, [Priority, Wave]
gh project item-edit --id "$ITEM" --project-id $PROJECT_ID \
  --field-id PVTSSF_lADOA9eCcc4AL09mzgHixXo --single-select-option-id 98cc1adc   # Status = Backlog
gh project item-edit --id "$ITEM" --project-id $PROJECT_ID \
  --field-id PVTSSF_lADOA9eCcc4AL09mzhYFN20 --single-select-option-id 0ccdae5c   # Kind = Feature
gh project item-edit --id "$ITEM" --project-id $PROJECT_ID \
  --field-id PVTSSF_lADOA9eCcc4AL09mzhYFN24 --single-select-option-id bc9222d4   # Area = Server

# 6. Link as a native sub-issue of the parent
PARENT_ID=$(gh issue view 10 --repo $REPO --json id --jq .id)
CHILD_ID=$(gh issue view "$NUM" --repo $REPO --json id --jq .id)
gh api graphql -f query='mutation($p:ID!,$c:ID!){ addSubIssue(input:{issueId:$p, subIssueId:$c}){ subIssue { number } } }' \
  -F p="$PARENT_ID" -F c="$CHILD_ID"
#    gh exits 0 even on GraphQL errors — inspect the response body for an "errors" array.
```

## Issue dependencies (blocked-by)

True execution blockers are recorded as **native GitHub issue dependencies** via the REST endpoint
(the "left blocks right" direction from roadmap §8). Use the blocking issue's **REST database id**
(the `.id` field), not its node id:

```bash
# Make #37 (X-03) blocked_by #29 (G-03):
BLOCKING_DBID=$(gh api repos/assimalign/ograph/issues/29 --jq '.id')
gh api -X POST repos/assimalign/ograph/issues/37/dependencies/blocked_by -F issue_id=$BLOCKING_DBID
# Verify: .issue_dependencies_summary.blocked_by should increment.
```

The nine edges recorded at initial population (blocking → blocked): N-08→N-01, G-03→X-03, G-06→V-01,
S-07→V-11, S-08→V-10, T-02→G-07, G-07→N-05, S-09→V-09, V-07→L-01. Everything else is expressed by
Wave/Priority only.

## Closing multiple work items from one PR

GitHub only auto-closes an issue when the PR body contains a **closing keyword + that issue's number**.
A single `Closes #1, #2` links only the first. Use **one keyword per issue, one per line**:

```
Closes #43
Closes #44
Closes #45
```

Closing a parent feature does **not** close its sub-issues, and closing every sub-issue does **not**
close the parent. List each work item the PR actually resolves. The generator can assemble this block:
`New-OGraphWorkItem.ps1 -EmitClosesBlock`.

## Built-in Project workflows — enable in the UI (one-time, not API-automatable)

GitHub's built-in Project automations are **not** exposed by the REST/GraphQL API or `gh` (only
`deleteProjectV2Workflow` exists), so this is the one step that must be done by hand. At
`https://github.com/orgs/assimalign/projects/8` → **⋯ menu → Workflows**, enable:

| Workflow | Set |
| --- | --- |
| Item added to project | Status → **Backlog** |
| Item closed | Status → **Done** |
| Pull request merged | Status → **Done** |
| Item reopened | Status → **In progress** |

With these on, closing/merging a PR moves every work item it `Closes` to Done automatically, so the
script's `-Status` only needs to seed the initial state.

## Re-discovering IDs if the schema changes

```bash
# Project node id
gh project view 8 --owner assimalign --format json --jq .id
# All single-select fields with their option ids
gh project field-list 8 --owner assimalign --format json \
  --jq '.fields[] | select(.type=="ProjectV2SingleSelectField") | {name, id, options:[.options[]|{name,id}]}'
# Area epics (excludes the .00 root by title pattern)
gh issue list --repo assimalign/ograph --state open --search '"OGraph -" in:title' --json number,title
```
