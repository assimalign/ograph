---
name: ograph-work-items
description: Create and link GitHub work items (area epics / features / tasks) in the assimalign/ograph Project #8 using the gh CLI — following the O01.01.NN WBS scheme, native parent/sub-issue links, and the Summary / Acceptance Criteria body template. Use whenever new development is requested, and ESPECIALLY when scope creep is discovered mid-branch: file the out-of-scope work as its own tracked work item so one PR can attach and close several items. Triggers include "create a work item", "file an issue for this", "capture this scope creep", "track this extra change", "open an OGraph issue", "this is out of scope — log it", or "assemble the Closes list for my PR". Use only in the assimalign/ograph repo.
---

# OGraph Work Items

Capture development as tracked GitHub work items in **Project #8 "OGraph"** under the `assimalign` org,
using the `gh` CLI. The defining use case is **scope-creep capture**: while implementing a feature you do
extra work that falls outside the original item; this skill turns that work into its own properly-placed
issue so the eventual PR closes *every* item it actually resolved — not just the one you started with.

Full schema, field IDs, and manual recipes live in [reference/project-schema.md](reference/project-schema.md).
The reliable end-to-end path is the script [scripts/New-OGraphWorkItem.ps1](scripts/New-OGraphWorkItem.ps1).

## The model (read this first)

Items carry their place in the title as `[<wbs>] <description>`; the tree is held by **native GitHub
parent/sub-issue links**; every item is added to Project #8.

| Code | Level | Example | Parent of new item |
| --- | --- | --- | --- |
| `O01.01.NN` (3 seg) | **Area epic** | `[O01.01.05] OGraph - Server` (#nnn) | parent for a new **feature** |
| `O01.01.NN.MM` (4 seg) | **Feature** | `[O01.01.05.01] …HTTP QUERY binding` | parent for a new **task** |
| `O01.01.NN.MM.PP` (5 seg) | **Task** | `[O01.01.05.01.01] …` (#nnn) | leaf |

Your branch names the feature in flight: `feature/O01.01.05.01-query-method` → feature `O01.01.05.01`.

## Step 1 — Classify the work before creating anything

When a piece of work surfaces, decide where it belongs. **This classification is the whole point** — getting
it right is what makes the WBS, the scope-creep accounting, and the multi-item PR meaningful. The script
infers an **Origin** from this choice so creep becomes measurable; the three buckets are:

- **In-scope of the current feature, but separable** → new **task** under the feature.
  Parent = the feature named by the branch. New code = `<branchWbs>.NN`. (`-As task`, the default.)
  → Origin **DiscoveredTask** — normal in-feature discovery.
- **Out of the current feature's scope, same area** → new **sibling feature** under the area epic.
  Parent = area epic (branch WBS minus its last segment). New code = `<areaWbs>.NN`. (`-As feature`.)
  → Origin **DiscoveredFeature** — a signal the original feature was under-scoped.
- **Different area entirely** → feature or task under *that* area's epic. Pass `-Parent` explicitly
  (find the area epic in [reference/project-schema.md](reference/project-schema.md)). Origin defaults to
  **Planned**; add `-Origin DiscoveredTask|DiscoveredFeature` if it is really creep.
- **Same intent as an existing item** → do **not** create a duplicate; reuse it.

If the classification is ambiguous, state your reasoning and ask the user which bucket it is before creating.

Before creating, **search for an existing item** so you don't duplicate — reuse beats re-filing. The script
ranks existing issues (open + closed) by title-word overlap:

```pwsh
.claude/skills/ograph-work-items/scripts/New-OGraphWorkItem.ps1 -Search "query method body binding"
```

If a strong match exists, add the work to it instead of creating a new item. You don't have to run `-Search`
separately: the create path **automatically** runs the same check and **blocks on a likely duplicate**
(open item, high overlap), printing the candidates — pass `-Force` only after you've confirmed the new item is
genuinely distinct.

## Step 2 — Create the work item (fast path)

Run the helper from the repo root. It infers the parent from the branch, computes the next free WBS child
code, creates the issue with a templated body, adds it to Project #8, sets fields, links the native
sub-issue, and records the item in a per-branch manifest for PR close-out.

```pwsh
# Scope-creep TASK on the current feature branch:
.claude/skills/ograph-work-items/scripts/New-OGraphWorkItem.ps1 -As task `
  -Title "Validate query Content-Type against RFC 10008 media-type rules" `
  -Summary "Hardening discovered while wiring the QUERY method binding; outside the original feature scope." `
  -Acceptance "Reject unsupported query media types with 415.","Add targeted unit tests.","NativeAOT-safe." `
  -Status "In progress"

# Out-of-feature → sibling FEATURE under the area epic:
.claude/skills/ograph-work-items/scripts/New-OGraphWorkItem.ps1 -As feature `
  -Title "Implement ETag validators and conditional QUERY caching" -Wave W04 -Priority P003

# Different area, explicit parent (e.g. Syntax area epic #nnn):
.claude/skills/ograph-work-items/scripts/New-OGraphWorkItem.ps1 -Parent "O01.01.03" `
  -Title "Add \$var parameter parsing to the filter grammar"

# Preview everything without creating (prints each gh command + the generated body):
.claude/skills/ograph-work-items/scripts/New-OGraphWorkItem.ps1 -Title "..." -DryRun
```

Key options: `-Search "<keywords>"` (find existing items, then exit), `-As task|feature`,
`-Parent <issue#|WBS>`, `-Origin Planned|DiscoveredTask|DiscoveredFeature` (inferred when omitted), `-Title`,
`-Summary`, `-Acceptance a,b,c`, `-Standards a,b`, `-BodyFile <path>`, `-Status` (default `Backlog`),
`-Priority`, `-Wave`, `-Label`, `-Force` (override the duplicate block), `-DryRun`.

The script also: **searches for duplicates** and blocks on a likely match (Step 1); **validates placement**
(refuses a parent that isn't an area epic or feature, and refuses to infer off a non-feature branch); sets
**Kind / Area / Origin** project fields, the **`scope-creep`** label on discovered items, and the
**`enhancement`** label on features automatically; stamps the native GitHub **issue type**
(feature → `Feature`, task → `Task`) so the item gets a title badge and can be grouped by the built-in
**Type** field; stamps discovered items with a
`> Discovered while implementing [<feature>] (#N)` provenance line; records each item (with its Origin)
in a per-branch manifest under `.git/ograph/`; and **backs off and retries on GitHub rate limits**
(honoring `Retry-After` / the reset, else exponential backoff).

**Run `-DryRun` first** when you're unsure about the inferred parent or the next WBS code, then re-run for real.

If PowerShell isn't available, follow the **manual recipe** in
[reference/project-schema.md](reference/project-schema.md) — same six steps, raw `gh`/`gh api graphql`.

## Step 3 — Body content

The repo's de-facto issue body is `## Summary` → `## Acceptance Criteria` → `### Standards and Compliance`.
The script generates this skeleton; fill it with real, testable criteria. For wire-protocol work, cite the
OGraph specification section it implements (and RFC 10008 / RFC 9110 / RFC 8941 where relevant) in
*Standards and Compliance*; for runtime-contract work, note that it is a runtime-contract concern and that
the change remains NativeAOT-safe and trimming-safe.

## Step 4 — Close every item from the PR

GitHub auto-closes an issue only when the PR body has a **closing keyword + that issue's number**, and
`Closes #1, #2` on one line links only the first. Use **one keyword per line**. The script tracks every item
it created on this branch (with its Origin); emit the block from the **same worktree** when opening the PR
(the manifest lives in `.git/ograph/`, so it is per-worktree):

```pwsh
.claude/skills/ograph-work-items/scripts/New-OGraphWorkItem.ps1 -EmitClosesBlock
```

The output groups planned vs discovered work and tallies the creep — paste it into the PR description
(alongside the original feature item, which you close manually):

```
## Work items resolved by this PR
Closes #43

### Discovered (out-of-scope) work
Closes #101
Closes #102

<!-- creep: 2 of 3 items were discovered out-of-scope -->
```

Closing a parent feature does not close its sub-issues and vice-versa, so list each work item the PR resolves.

## Guardrails

- **Only operate on `assimalign/ograph`** and Project #8. Confirm `gh auth status` has the `project` scope.
- **Never invent a WBS code** — always derive the next child from existing siblings (the script does this; if
  doing it by hand, list the parent's children and take max+1, zero-padded to two digits).
- **One concern per item.** If the creep is really several things, create several items.
- **Don't create duplicates** — search first (Step 1).
- **Confirm before creating** when the parent or classification is uncertain; prefer `-DryRun` to preview.
- **Set Status.** Use `In progress` if you're starting the work now, else `Backlog`/`Ready`.
- This skill is intake/tracking only — it does not change code. Follow the OGraph delivery roadmap
  (`docs/DELIVERY_ROADMAP.md`) and `.claude/rules/workflow.md` for the actual implementation.
