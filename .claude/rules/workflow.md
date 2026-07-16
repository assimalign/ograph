# Workflow Conventions

## Commit messages — conventional commits

```
type(scope): subject
```

**Types:** `feat`, `fix`, `docs`, `refactor`, `test`, `chore`.
Examples: `feat(server): add HTTP QUERY method binding` · `fix(syntax): resolve filter-precedence parse error` · `chore(build): update to .NET 10.0.101`

## Branch naming

- `main` — production-ready · `development` — integration
- `feature/{name}` — new features. Work tracked in the OGraph GitHub Project uses `feature/<wbs>-<slug>` (e.g., `feature/O01.01.05.01-query-method`)
- `fix/{name}` — bug fixes · `docs/{name}` — documentation

## GitHub Project execution metadata

Treat OGraph GitHub Project fields as execution guidance, not decorative labels:

- `Priority`: lower number = higher priority (`P001` before `P002`). `Wave`: lower number = earlier delivery (`W01` before `W02`).
- When selecting work autonomously, prefer items that are both unblocked and in the earliest available `Priority` and `Wave`. Do not pull later-wave work ahead of earlier-wave blockers unless the user asks or the dependency graph requires it.
- Conflicts resolve in this order: explicit user instruction → dependency/blocker relationships → `Priority` → `Wave`.
- Preserve later-wave requirements in planning notes even when implementing only current-wave scope. If a ticket needs prerequisite work from another ticket, call that out rather than silently reordering.
- Work items follow `[<wbs>] <title>` (area epic `O01.01.NN` → feature `O01.01.NN.MM` → task `O01.01.NN.MM.PP`) in Project #8. Use the `ograph-work-items` skill to create, place, and link items — especially for capturing scope creep discovered mid-branch.
- When implementing, the GitHub issue body is the authoritative source of service-specific requirements — keep the implementation aligned with it alongside the roadmap (`docs/DELIVERY_ROADMAP.md`) and the repo-wide conventions.

## Backlog authoring

When creating or refining backlog items, include architectural boundary guidance that helps a future implementation session: suggested project families (candidate names, each project's responsibility, dependency direction — advisory unless marked required), and boundaries that matter for AOT, source generation, validation, serialization, transport, or host integration. Every shipped library must be NativeAOT-compatible, and this repo codes for **no framework at all** (host adapters live outside it — see `docs/DELIVERY_ROADMAP.md` §D5). Use issue bodies to preserve this context even when placeholder folders already exist.

The roadmap at `docs/DELIVERY_ROADMAP.md` is the source of truth for the backlog: WBS taxonomy (§3), priority/wave scales (§4), the area-epic and feature list (§5), Project #8 configuration (§6), and the work-item process artifacts (§7).
