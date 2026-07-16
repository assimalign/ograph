# OGraph Delivery Roadmap

> Status: **DRAFT — pending review.** Once ratified, this document is the source of truth for the
> GitHub Project #8 "OGraph" backlog (org `assimalign`). It mirrors the iterative-development model
> proven on `assimalign/cohesion` Project #13.

OGraph solves a **convention problem, not a coding problem**: it combines the query-resolution
capabilities of GraphQL and the model-exchange discipline of OData while remaining REST over HTTP.
Every shipped library **must be NativeAOT-compatible**.

---

## 1. Current-state assessment (2026-07-16)

| Area | State | Evidence |
| --- | --- | --- |
| Specification | Skeleton with strong bones; many stub sections | `.designing/ograph-specfication.md` — GDM elements + policy model + reserved `$.query` defined; Scalar/Enum/Collection/Entity rules, functions, operators, bindings, and the entire Communication section are empty |
| GDM layering proposal | Complete, not yet ratified | `.designing/gdm.proposal.md` — 3-layer split (Core GDM / Capability model / Protocol bindings), edge cardinality, capability results, parameter roles |
| Gdm library | Large, half-finished, **does not compile** | 243 files / ~11.7k lines; hard syntax errors in `Descriptors/GdmEntityTypeDescriptor.cs` (line 34: `new GdmProperty(propertyInfo.Name,)`) block Gdm → Core → Server → Client builds; **108 `NotImplementedException` hits** (~69 in compiled element/type-system code, ~25 in `Descriptors/**`, 14 in excluded/test files); 16 hard-reflection call sites (`Activator.CreateInstance`/`MakeGenericType`/`Expression.Lambda`) + ~41 broader `System.Reflection` usage lines vs. the AOT mandate |
| Syntax library | Most mature; genuinely tested | ~7k lines; active `QueryParser.*` + full AST + diagnostics (G0000–G0008); filter/sort paths and the base `QueryVisitor` unfinished; superseded `Internal/Parsers/**` excluded from compile; `QueryableQueryVisitor` already emits IL3050 AOT warnings |
| Server library | Scaffolded pipeline, protocol gaps | Custom HTTP abstraction with no framework dependency — **by design (see D5)**, but the hosting contract is neither finalized nor documented; query read **from query string only** (`Internal/OperationBindingMiddleware.QueryParsing.cs`), request body never read; empty invalid-query branch + no-op catch in that same file; `Executor.cs` has a timeout self-comparison bug, a rethrow-only catch, and empty 415/406 negotiation branches; a large `<Compile Remove>` block is commented out (all files compile) |
| Core library | Thin (error primitives + re-export of Gdm/Syntax) | 8 files / ~100 lines; `ValueObjects/**` excluded from compile |
| Client library | Stub | Interfaces + LINQ provider that is 100% `NotImplementedException`; test project empty |
| ToolKit + analyzers | Functional and real | Type-utility attributes, change tracking, incremental source generators + code fixes with real tests — this is the intended AOT-safe path; one generator (`EntityKeyAttribute`) compile-excluded |
| Sdk / Build.Tasks | MSBuild skeleton only | No source |
| Cli | Empty stub | `Program.cs` is 8 lines |
| Extensions (VS / VSCode) | Template scaffolds | VSIX template; TS LSP + Vue editor scaffold; committed `out/` build artifacts |
| CI | Outdated | Five per-library workflows on .NET 6/7/8, actions v1/v2, wrong paths filters; repo now targets net10.0 |
| Solution | Dual, partially stale | `Assimalign.OGraph.slnx` authoritative (22 projects); old `.sln` references nonexistent projects incl. a once-planned `AspNetCore` integration (now explicitly abandoned per D5) |

**Reading:** the protocol's differentiators (policy-governed query capability, edge-level partial
results, model exchange) are designed but not yet specified to completion, and the implementation
inverted the ideal order — breadth of scaffolding before the spec stabilized. The plan below
re-sequences: **restore the baseline and converge the spec first (W01), then finish the engine
inside-out (Gdm → Syntax → Server), then the surface area (Client, SDK, CLI, extensions).**

## 2. Design decisions this roadmap ratifies

These are the decisions embedded in the backlog. Approving this roadmap approves these.

### D1 — Adopt the 3-layer GDM (gdm.proposal.md)
Core GDM (Graph/Node/Edge/Type) is transport-neutral; a Capability model (Query/Command/Event/
Subscription with `Result`+`Cardinality`, semantic parameter roles) expresses executable intent;
Protocol Bindings own HTTP (method, route, parameter source, media types). The current spec's
`<Query Node=".."/>` + `$.query`-parameter-with-policies shape is **retained** as the capability
description; policy resolution rules (property-level ∩ operation-level) stay as specified.
Terminology canonicalizes on **Node** (Vertex = query-language alias only).
**Document format:** the single-file `gdmx` design is adopted — Model/Types/Graphs/Capabilities/
Bindings live in one document; the proposal's split formats (`gcapx`/`gbindx`) are deferred
indefinitely.

### D2 — RFC 10008: the OGraph query moves into the request body
[RFC 10008](https://datatracker.ietf.org/doc/rfc10008/) (June 2026) standardizes the HTTP **QUERY**
method: safe, idempotent, cacheable, body-carrying. This fits OGraph precisely: it removes the
URL-length and percent-encoding problems of `?query=` and gives body-carried queries a
*standardized* caching model (previously impossible with POST). Note: shared-cache/CDN support for
QUERY is still emergent; the design leans on it for correctness, not for day-one performance.

Binding changes:
1. **Primary read binding:** `QUERY /{domain}/{node}` with the OGraph query text as the request body.
2. **Media types (working names, vendor tree):** request body `application/vnd.ograph.query`
   (UTF-8 fixed by definition); response envelope `application/vnd.ograph+json`. Final naming and
   the registration path (lightweight vendor-tree registration vs. pursuing IETF standards-tree)
   are decided in S-05 — standards-tree names like `application/ograph-query` cannot simply be
   claimed (RFC 6838 §3.1).
3. **Status taxonomy (per RFC 10008 + RFC 9110):** missing `Content-Type` → **400**; present but
   unsupported query media type → **415**; supported media type but body inconsistent with it →
   **400**; well-formed query that fails semantic/policy validation → **422** with the diagnostics
   payload.
4. **Discovery:** servers advertise `Accept-Query` (an RFC 8941 Structured Field list of supported
   query media types, **no media-type parameters**, scoped to the path — query component ignored)
   on responses from bound resources, incl. `OPTIONS`.
5. **Caching/conditionals:** RFC 10008 binds *caches* to key QUERY responses on the request content
   + related metadata; the *server's* obligations are emitting validators (ETag) and correct
   `Cache-Control` metadata. Conditional headers behave as for GET. `no-transform` and
   normalization risks are specified in S-05.
6. **Re-fetch affordances:** `Location` / `Content-Location` on 2xx let clients re-fetch results
   via plain GET without resending the body — specified in S-05, implemented in V-08/L-01.
7. **Compatibility binding:** `GET /{node}?query=` demoted to an OPTIONAL fallback binding
   implementations MAY expose; the conformance suite covers it as an optional profile (browsers,
   CORS-preflight-averse clients, and QUERY-unaware intermediaries still need it).
8. **Commands:** unchanged (POST/PUT/PATCH/DELETE with input payload as body). Where a Command
   supports `$.query` (e.g. projection of the result), placement is **decided by S-07**; the
   default candidate is a reserved member of the command payload envelope. A custom request header
   is the fallback only if S-07 resolves its field-value encoding and size-limit problems.

### D3 — AOT via source generation, not reflection
`IsAotCompatible=true` already enables the trim/AOT analyzers (IL3050 warnings fire in Syntax
today). The work is therefore: **burn down the existing warning backlog, promote `IL*` warnings to
errors (W02), and land a `PublishAot` smoke host in CI (W04)**. Gdm's reflection-based descriptor
path is displaced by ToolKit source-generated model metadata — and to avoid building the engine
twice, the source-generated construction path is developed *alongside* the Gdm runtime in W02
(T-02), not after it.

### D4 — Replicate the Cohesion work-item system
WBS-coded issues, native sub-issue hierarchy, scope-creep capture with Origin classification, and
a `New-OGraphWorkItem.ps1` helper + `ograph-work-items` skill (see §6–§7).

### D5 — Framework-neutral hosting: no framework integration ships from this repo
OGraph will sit on top of the **Cohesion framework** in practice, but this repo codes for **no
framework at all** — not ASP.NET Core, and deliberately not Cohesion either. The Server library's
own HTTP abstraction (`IOGraphExecutorRequest`/`IOGraphExecutorResponse` + value objects) **is** the
public hosting contract: any host adapts its request/response types to it. Consequences:
1. The previously planned `Assimalign.OGraph.Server.AspNetCore` package is **dropped**; host
   adapters (Cohesion, ASP.NET Core, or anything else) live outside this repo (e.g.
   `cohesion-integrations`).
2. The hosting contract is finalized, versioned, and documented with an **adapter guide** (V-07):
   what a host must supply (method/path/headers/body streams, QUERY method pass-through), what the
   executor guarantees, and which concerns stay host-side (TLS, HTTP version, connection
   management, CORS enforcement).
3. End-to-end verification uses a **test-only reference host** built on BCL primitives
   (`System.Net.HttpListener` — no framework dependency, never shipped as a package), which is what
   the conformance suite (N-03) and the AOT smoke host (N-05) run against.

## 3. WBS taxonomy

Identical mechanics to Cohesion, new namespace:

| Code shape | Level | Title pattern | Example |
| --- | --- | --- | --- |
| `O01.01.00` | Program root | `[O01.01.00] OGraph Protocol & Libraries` | — |
| `O01.01.NN` | **Area epic** | `[O01.01.NN] OGraph - <Area>` | `[O01.01.05] OGraph - Server` |
| `O01.01.NN.MM` | **Feature** | `[O01.01.NN.MM] <imperative>` | `[O01.01.05.01] Implement HTTP QUERY method binding` |
| `O01.01.NN.MM.PP` | **Task** | `[O01.01.NN.MM.PP] <imperative>` | leaf |

The program root deliberately has **no** `OGraph - ` prefix so the area-title regex
(`OGraph - (.+)$`) never matches it; epic-discovery searches must still exclude the `.00` root by
code. Branch convention: `feature/<wbs>-<slug>` (e.g. `feature/O01.01.05.01-query-method`).
Hierarchy is held by **native GitHub parent/sub-issue links**; every item joins Project #8.

## 4. Priority scale and waves

**Priority** `P001` (highest) → `P007` (lowest) — same semantics as Cohesion.

**Waves** (iterations; lower = earlier; a feature's wave is when it is *expected* to start).
Exit criteria are concrete artifacts, not vibes:

| Wave | Theme | Exit criterion |
| --- | --- | --- |
| **W01** | Baseline + spec convergence | Tree compiles on net10.0 and modernized CI is green on build+existing tests; spec artifacts committed: reconciled GDM sections, query-language **EBNF file**, response-envelope **JSON Schema**, and **≥10 golden request/response fixtures** (these seed N-03) |
| **W02** | Engine core | Gdm element model + capability model + validation complete; parser + semantic analysis complete for filter/sort/page/project; Server ingests QUERY bodies (integration-tested against the fixture set); source-gen model path walking; `IL*` warnings promoted to errors |
| **W03** | End-to-end vertical slice | `QUERY /hrm/employees` with filter/sort/page/project + edge traversal returns the spec envelope through the reference test host; commands execute; Client MVP consumes it; conformance suite runs |
| **W04** | AOT hardening + model exchange | `PublishAot` smoke host passes in CI; reflection descriptors displaced/deleted; metadata endpoint serves the GDM |
| **W05** | Developer surface | SDK targets, CLI codegen/validate, batch/transaction spec |
| **W06** | Ecosystem | VSCode LSP, benchmarks, docs site, VS extension, batch/transaction impl |

W02 is deliberately the heaviest wave (the engine); its chains (Gdm / Syntax / Server) run in
parallel and the board — not this document — governs any mid-wave rebalancing.

## 5. Area epics and feature backlog

Twelve area epics under the program root. Features listed with target Wave/Priority.
(Tasks are intentionally *not* pre-decomposed — per the iterative model they are filed as
`DiscoveredTask` items while a feature is in flight.)

### O01.01.01 — OGraph - Specification
| # | Feature | Wave | Pri |
| --- | --- | --- | --- |
| S-01 | Reconcile the GDM spec with the layering proposal (ratify D1; resolve Policy-vs-Directive split; canonicalize Node/Vertex; adopt single-file gdmx) | W01 | P001 |
| S-02 | Complete the type-system sections (Scalar, Enum+Member, Collection+Item, Entity+Key, Function signatures, nullability/read-only semantics) | W01 | P002 |
| S-03 | Specify the query language grammar (**committed EBNF**): filter operators/precedence, string/array/numeric functions, literals, variables (`$var`) | W01 | P002 |
| S-04 | Finalize edge-traversal syntax (`.edge(e-[has]->a:alias)` vs `.edge(node:/edge)` divergence in samples) and traversal scoping rules | W01 | P002 |
| S-05 | Communication spec: RFC 10008 QUERY binding (D2) — media-type naming+registration path, 400/415/422 status taxonomy, Accept-Query (RFC 8941) semantics, caching/conditionals/no-transform, Location/Content-Location, CORS-preflight note, GET fallback profile | W01 | P001 |
| S-06 | Response envelope spec (**JSON Schema + golden fixtures**): `$nodes`/`$edges` composition, per-edge `$status`/`$errors` partial-failure semantics, paging metadata (`$count`/`$total`/cursors) | W01 | P002 |
| S-07 | Command query-control placement (envelope member vs header — decide and specify) + command binding conventions | W02 | P002 |
| S-08 | Model-exchange (metadata) endpoint spec — serve the GDM over the web (OData `$metadata` equivalent) | W03 | P003 |
| S-09 | Batch and transaction spec (`$batch`, `$transaction`, strategies, atomicity) | W05 | P004 |
| S-10 | Model stitching design (existing issue #1 — adopt into WBS) | W06 | P005 |
| S-11 | Pub/Sub + Events/Subscriptions modeling design (existing issue #2 — adopt into WBS) | W06 | P005 |

### O01.01.02 — OGraph - Gdm
| # | Feature | Wave | Pri |
| --- | --- | --- | --- |
| G-01 | Stabilize the core element model per D1 (Graph/Node/Edge/Type; edge cardinality + edge-owned properties; type references with Cardinality/Nullable; resolve the Gdm `<Compile Remove>` groups: `.dev` types, `Internal/Elements`, `System`, `GdmEdge.T1.T2`) | W02 | P001 |
| G-02 | Complete the type-system runtime (scalar/enum/complex/entity/collection read-write contracts — the ~69 `NotImplementedException`s in compiled element/type code; the ~25 in `Descriptors/**` are resolved by displacement under G-07, not here) | W02 | P001 |
| G-03 | Implement the capability model (Query/Command elements, parameters + semantic roles, `$.query` policies incl. reserved `$.filter`/`$.sort`/`$.project`/`$.page`) | W02 | P002 |
| G-04 | Gdmx serialization: read/write the single-file XML model document with versioning (`GdmSerializer.Version1` completion, incl. Capabilities + Bindings sections) | W03 | P002 |
| G-05 | Model validation engine (all MUST rules from the spec: uniqueness, reference resolution, policy narrowing `operation ⊆ property`) | W02 | P002 |
| G-06 | Protocol-binding model (HttpBinding: method/route/parameter-source/media types) — precedes the Server binding surface per D1 layering | W02 | P002 |
| G-07 | Displace/delete the reflection descriptor path: consume ToolKit source-generated metadata (from T-02); annotate or remove remaining reflection (D3) | W04 | P001 |
| G-08 | Directive/annotation model (`Authorize`, `Description`, custom directives with usage targets) | W04 | P003 |

### O01.01.03 — OGraph - Syntax
| # | Feature | Wave | Pri |
| --- | --- | --- | --- |
| X-01 | Complete filter-expression parsing (binary operators, precedence, grouping, `any`/`all` lambda forms, functions) | W02 | P001 |
| X-02 | Complete sort/page/project parsing edge cases + query variables (`$var`) and parameterized queries | W02 | P002 |
| X-03 | Semantic analyzer: resolve AST against the GDM (root node, member/type resolution, edge chains, policy availability enforcement) — blocked by G-03 | W02 | P001 |
| X-04 | Finish the visitor layer (`QueryVisitor` base, `QueryableQueryVisitor` → LINQ expression trees; resolve its IL3050 warnings) | W03 | P002 |
| X-05 | Diagnostics expansion + error recovery (stable G-codes, positions, multi-error parses for editor scenarios) | W03 | P003 |
| X-06 | Delete the superseded `Internal/Parsers/**` generation and resolve the Syntax `<Compile Remove>` exclusions | W03 | P003 |

### O01.01.04 — OGraph - Core
| # | Feature | Wave | Pri |
| --- | --- | --- | --- |
| C-01 | Finalize the error model (error codes ↔ response-envelope `$errors` alignment; exception hierarchy) | W02 | P002 |
| C-02 | Resolve Core's role: aggregation surface vs. real abstractions; fix or delete excluded `ValueObjects/**` | W03 | P003 |

### O01.01.05 — OGraph - Server
| # | Feature | Wave | Pri |
| --- | --- | --- | --- |
| V-01 | HTTP QUERY method support: add `Method.Query`, implement the existing `MapQuery` declaration in `ApplicationOperationDescriptor`, wire executor matching (D2) | W02 | P001 |
| V-02 | Request-body query ingestion: read `application/vnd.ograph.query` bodies, 400/415 Content-Type taxonomy, request `Content-Encoding` handling, size limits | W02 | P001 |
| V-03 | Fix known pipeline defects: `Executor.cs` timeout self-comparison + rethrow-only catch; `OperationBindingMiddleware.QueryParsing.cs` empty invalid-query branch + no-op catch → 400 (syntax) / 422 (semantic) with diagnostics payload | W02 | P001 |
| V-04 | Content negotiation completion (Accept → 406, `Accept-Query` advertisement, OPTIONS handling; CORS is host-side per D5 — documented in the V-07 adapter guide) | W03 | P002 |
| V-05 | Response envelope serialization (`$nodes`/`$edges`, per-edge status/errors, paging metadata; Utf8JsonWriter, AOT-safe) — needs S-06 + C-01, not X-04 | W03 | P001 |
| V-06 | Edge-resolver execution pipeline (resolver operations, traversal scoping, parallel resolution, partial-failure capture) | W03 | P001 |
| V-07 | Framework-neutral hosting contract (D5): finalize + document the `IOGraphExecutor*` integration surface and adapter guide; build the test-only BCL reference host (`HttpListener`) used by N-03/N-05 — no framework package ships | W03 | P001 |
| V-08 | Caching & conditional requests (ETag/validators, Cache-Control, Location/Content-Location re-fetch, RFC 10008 GET-equivalent selected representation) | W04 | P003 |
| V-09 | Batch/transaction endpoint execution (implements S-09) | W06 | P004 |
| V-10 | Model-exchange (metadata) endpoint — serve the gdmx document (implements S-08) | W04 | P002 |
| V-11 | Command execution binding: POST/PUT/PATCH/DELETE input deserialization + `$.query` projection per S-07 | W03 | P002 |

### O01.01.06 — OGraph - Client
| # | Feature | Wave | Pri |
| --- | --- | --- | --- |
| L-01 | HTTP client implementation: send QUERY with body via `HttpClient` (custom method), handle envelope + partial failures + Location/Content-Location | W03 | P002 |
| L-02 | Client factory + DI integration (`IOGraphClientFactory`) | W03 | P003 |
| L-03 | Typed response materialization (source-gen deserialization, AOT-safe) | W04 | P002 |
| L-04 | Query builder / LINQ provider (`OGraphQueryable`) | W05 | P004 |

### O01.01.07 — OGraph - ToolKit
| # | Feature | Wave | Pri |
| --- | --- | --- | --- |
| T-01 | Finish TypeUtilities generators (re-enable excluded `EntityKeyAttribute` generator; complete Omit/Pick) | W02 | P002 |
| T-02 | **GDM compile-time model generation** from CLR types — the AOT keystone; walking-skeleton lands in W02 alongside G-01/G-02 so the Gdm runtime is built against the source-gen path, not the reflection path | W02 | P001 |
| T-03 | Change-tracking polish + docs (`ToolKit.Gdm`) | W05 | P004 |

### O01.01.08 — OGraph - Analyzers
| # | Feature | Wave | Pri |
| --- | --- | --- | --- |
| A-01 | Diagnostic/code-fix coverage for all ToolKit attributes (mixing rules, ctor requirements) | W04 | P003 |
| A-02 | AOT-misuse analyzers (flag reflection-dependent Gdm APIs when consumer targets AOT) | W05 | P004 |

### O01.01.09 — OGraph - Sdk
| # | Feature | Wave | Pri |
| --- | --- | --- | --- |
| K-01 | MSBuild SDK: props/targets wiring generators + gdmx validation into consumer builds | W05 | P004 |
| K-02 | Build.Tasks implementation (model validation at build time) | W05 | P004 |

### O01.01.10 — OGraph - Cli
| # | Feature | Wave | Pri |
| --- | --- | --- | --- |
| I-01 | CLI command framework + `ograph codegen` (gdmx → C#/clients) | W05 | P004 |
| I-02 | `ograph validate` (model + query validation against a gdmx) | W05 | P004 |

### O01.01.11 — OGraph - Extensions
| # | Feature | Wave | Pri |
| --- | --- | --- | --- |
| E-01 | VSCode LSP for `.ograph`/`.gdmx` (reuse Syntax diagnostics) | W06 | P005 |
| E-02 | Visual Studio extension (language service) | W06 | P006 |
| E-03 | Web editor/visualizer alignment (`ograph-web-editor` repo) | W06 | P006 |

### O01.01.12 — OGraph - Engineering
| # | Feature | Wave | Pri |
| --- | --- | --- | --- |
| N-01 | CI modernization: net10.0, actions v4, matrix builds, shared composite build action (Cohesion pattern), correct paths filters, `.slnx` only — blocked by N-08 | W01 | P001 |
| N-02 | Promote existing trim/AOT analyzer warnings to errors (`WarningsAsErrors` for `IL*`) and burn down the backlog (IL3050 already firing in Syntax) | W02 | P002 |
| N-03 | Conformance test suite: executable spec fixtures (model + query + expected envelope, seeded by W01 golden fixtures) runnable against any server implementation; includes the optional GET-fallback profile | W03 | P002 |
| N-04 | Mechanical solution hygiene: delete stale `.sln`, remove committed build artifacts (`extensions/**/out`), strip commented-out csproj blocks, and produce the exclusion inventory assigning each active `<Compile Remove>` to its owning feature (G-01, X-06, C-02, T-01) | W01 | P002 |
| N-05 | `PublishAot` smoke host in CI (sample HRM graph on the reference test host; boots and answers a QUERY request over HTTP/1.1; HTTP/2/3 verification belongs to host adapters outside this repo per D5) | W04 | P002 |
| N-06 | Benchmarks (parser + end-to-end; seed `ograph-benchmarks`) | W06 | P005 |
| N-07 | Documentation: getting-started rewrite, per-library OVERVIEW/DESIGN docs | W05 | P004 |
| N-08 | **Baseline: restore a compiling tree on net10.0** (fix `GdmEntityTypeDescriptor.cs` syntax errors and any remaining hard breaks; the 22-project slnx builds) | W01 | P001 |
| N-09 | OpenAPI 3.2 description of OGraph bindings (QUERY method describability) | W06 | P006 |

**Scale:** 1 program root + 12 area epics + 63 features = **76 issues** at initial population.

## 6. GitHub Project #8 configuration

Current state: empty; only default `Status` (Todo / In Progress / Done).

| Field | Type | Options |
| --- | --- | --- |
| Status | single-select (existing, options replaced) | `Backlog`, `Ready`, `In progress`, `In review`, `Done` |
| Kind | single-select (new) | `Program`, `Area Epic`, `Feature`, `Task` |
| Area | single-select (new) | `Specification`, `Gdm`, `Syntax`, `Core`, `Server`, `Client`, `ToolKit`, `Analyzers`, `Sdk`, `Cli`, `Extensions`, `Engineering` |
| Origin | single-select (new) | `Planned`, `DiscoveredTask`, `DiscoveredFeature` |
| Priority | single-select (new) | `P001` … `P007` |
| Wave | single-select (new) | `W01` … `W06` |

Labels: `scope-creep` (discovered items — applied by the script); `enhancement` on features is
applied by the issue form and, as an OGraph adaptation, by the script too.

**Manual step (UI-only, not API-automatable):** enable built-in workflows on Project #8 —
Item added → Backlog; Item closed → Done; PR merged → Done; Item reopened → In progress.

Existing issues **#1 (Model Stitching Design)** and **#2 (Pub/Sub Modeling Design)** are adopted as
features S-10/S-11: retitled with their WBS codes, added to Project #8, linked under the
Specification epic.

## 7. Work-item process artifacts (Cohesion replication)

| Artifact | Adaptation |
| --- | --- |
| `.claude/skills/ograph-work-items/SKILL.md` | Cohesion skill retargeted: repo `assimalign/ograph`, Project #8, WBS prefix `O01.01`, epic title pattern `OGraph - <Area>`; guardrails reference OGraph conventions, not `.claude/rules/` paths that don't exist here yet |
| `.claude/skills/ograph-work-items/reference/project-schema.md` | Regenerated from live Project #8 after field setup (ids captured by the discovery commands) |
| `.claude/skills/ograph-work-items/scripts/New-OGraphWorkItem.ps1` | Port of `New-CohesionWorkItem.ps1`. **Exhaustive change list — grep-replace every occurrence, don't spot-edit:** constants `$Owner`/`$Repo`/`$ProjectNum` (→ `assimalign/ograph`, 8); the WBS regex `L\d{2}(?:\.\d{2})+` at **both** sites (branch parse in `Get-BranchWbs` ~line 208 AND parent-title parse ~line 408) → `O\d{2}(?:\.\d{2})+`; area-title regex `Foundation - (.+)$` → `OGraph - (.+)$`; manifest dir `.git/cohesion/` → `.git/ograph/`; default Standards bullet (~line 339) → cite the OGraph spec / repo conventions; comment-based help + all examples rewritten to O-codes and the new script name; add auto-`enhancement` label for features |
| `.claude/rules/workflow.md` (new) | OGraph port of Cohesion's execution-metadata rules: Priority/Wave selection ordering, conflict resolution (user instruction → blockers → Priority → Wave), branch WBS convention, skill pointer, backlog-authoring guidance — retargeted to Project #8 / `O01.01` |
| `.github/ISSUE_TEMPLATE/{feature,task,scope-creep}.yml` + `config.yml` | Title prefixes `[O01.01.NN.MM]`/`[…PP]`; Standards defaults reference the OGraph spec + AOT criterion; contact link points at the skill |
| `.github/pull_request_template.md` | Cohesion template with OGraph checklist (AOT-safe, spec-section reference for protocol work); embedded generator command repointed to `.claude/skills/ograph-work-items/scripts/New-OGraphWorkItem.ps1 -EmitClosesBlock`; Cohesion-specific checklist bullets (`CohesionProjectReference` etc.) removed |

## 8. Sequencing and dependency map

```
W01  N-08 ──► N-01          N-04
     S-01 ──► S-02 / S-03 / S-04 / S-05 / S-06

W02  S-02/S-03 ──► X-01/X-02 ──► X-03 ◄── G-03
     S-01 ──► G-01 ──► G-02 / G-03 / G-05 / G-06
     S-05 ──► V-01/V-02      G-06 ──► V-01/V-02
     T-01 ──► T-02 (skeleton)      V-03, C-01, N-02, S-07

W03  X-03/X-04 ──► V-06      S-06/C-01 ──► V-05      V-05/V-06 ──► V-07 ──► L-01/L-02
     S-07 ──► V-11           S-05/S-06 ──► N-03      G-04, V-04, X-05, X-06, C-02, S-08

W04  T-02 ──► G-07 ──► N-05      S-08 ──► V-10      L-03, V-08, G-08, A-01

W05  K-01/K-02, I-01/I-02, L-04, T-03, A-02, N-07, S-09

W06  S-09 ──► V-09      E-01/E-02/E-03, N-06, N-09, S-10/S-11
```

**Edges that become native GitHub issue dependencies at population time** (true execution blockers
only, per the Cohesion rule): N-08→N-01, G-03→X-03, G-06→V-01, S-07→V-11, S-08→V-10, T-02→G-07,
G-07→N-05, S-09→V-09, V-07→L-01. Everything else is expressed by Wave/Priority only.

## 9. Execution notes

- Initial population (fields, epics, features, links, dependencies, existing-issue adoption) is
  delegated to an agent; every mutation via `gh` CLI mirrors the manual recipe in the skill
  reference.
- After population, `reference/project-schema.md` is regenerated from the live board so the skill
  documents real field/option ids.
- Scope creep from this point forward flows through `New-OGraphWorkItem.ps1` (Origin taxonomy),
  keeping the board honest as the greenfield design evolves.
