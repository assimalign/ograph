# OGraph Specification

> **Status:** Working draft. This revision ratifies design decision **D1** of the
> [delivery roadmap](DELIVERY_ROADMAP.md): the specification is organized into three layers —
> **Core GDM** (structural), **Capability Model** (behavioral), and **Protocol Bindings**
> (transport) — with canonicalized terminology and a single-file `gdmx` document format.
> Sections marked *[Owned by …]* are intentionally incomplete skeletons; the referenced
> roadmap feature completes them. Appendix A records every construct this revision supersedes;
> Appendix B records deliberate deltas from the ratified layering proposal.

# 1.0 — Introduction

The Open Graph Protocol (OGraph) is an application-level protocol derived from graph theory to
implement queryable and interoperable REST services over HTTP while adhering to Domain-Driven
Design concepts. This specification defines the core semantics and behavior of the OGraph
protocol. OGraph seeks to solve a **convention problem rather than a code problem**.

## 1.1 — Terminology

The key words "MUST", "MUST NOT", "REQUIRED", "SHALL", "SHALL NOT", "SHOULD", "SHOULD NOT",
"RECOMMENDED", "MAY", and "OPTIONAL" in this document are to be interpreted as described in
[RFC 2119](https://www.rfc-editor.org/rfc/rfc2119).

### 1.1.1 — Node and Vertex

The canonical term for a graph anchor in this specification, in the GDM document format, and in
all runtime APIs is **Node**. The term **Vertex** is retained *only* as a query-language synonym:
`vertex(...)` and `v(...)` in OGraph query text are aliases of `node(...)` and `n(...)`. A GDM
document MUST NOT use `Vertex` as an element or attribute name.

### 1.1.2 — Policy and Directive

Earlier drafts used *Policy* and *Directive* interchangeably. They are now distinct concepts:

| Concept | Purpose | Declared on | Effect on queries |
| --- | --- | --- | --- |
| **Policy** | Query-capability contract: which members, operators, functions, and options may be used | `Property` (§2.3.6), and the reserved `$.query` parameter (§3.3.2) | Property-hosted policies **bound** a member's capability; `$.query`-hosted policies **gate** keyword availability (§3.3.3) |
| **Directive** | Annotation and extension metadata (e.g. authorization, description, vendor hints) | Any element its `Usage` targets permit | **Never grants or removes** query capability; consumed by implementations and tooling |

Reserved-name rules:

1. A `$.`-prefixed name in a **Policy position** always denotes a reserved Policy
   (`$.filter`, `$.sort`, `$.project`, `$.page`).
2. A `$.`-prefixed name in a **Parameter position** always denotes a reserved protocol
   parameter (§3.2 rule 2). This revision reserves exactly one: `$.query`.
3. Directive names MUST NOT begin with `$.`.

The construct `<Directive Type="$.filter">` from earlier model drafts is superseded: reserved
`$.` names in Policy positions are always Policies.

## 1.2 — Layering

This specification is organized in three layers. Lower layers MUST NOT depend on higher layers.

1. **Core GDM (§2)** — the transport-neutral structural model: graphs, nodes, edges, types.
   Sufficient for schema exchange, projection analysis, and traversal planning.
2. **Capability Model (§3)** — executable intent over the structure: queries, commands, events,
   and subscriptions, with parameters, policies, and results. Sufficient for query validation and
   execution planning.
3. **Protocol Bindings (§4)** — the mapping of capabilities onto a transport (HTTP): methods,
   routes, parameter sources, and media types. Nothing in §2 or §3 references HTTP concepts.

## 1.3 — Document format (`gdmx`)

The document format is layer-neutral packaging: it carries all three layers in one file while
keeping them textually and semantically separate.

A **model** is the set of graphs declared by one `gdmx` document. Composing a model from
multiple documents (model stitching) is out of scope for this revision.
*[Owned by S-10 [O01.01.01.10].]*

```xml
<Gdmx Version="1.0">
  <Graph Domain="{domain}" Alias="{alias}">
    <!-- structural elements (§2) and capability elements (§3) -->
  </Graph>
  <Bindings Graph="{domain-or-alias}">
    <!-- protocol bindings (§4) -->
  </Bindings>
</Gdmx>
```

**Rules**
1. A `gdmx` document MUST have exactly one `<Gdmx>` root with a `Version` attribute.
2. Structural and capability elements are declared inside their owning `<Graph>`.
3. Protocol bindings are declared only in `<Bindings>` sections. The `Graph` attribute of a
   `<Bindings>` section MUST equal the `Domain` or `Alias` of exactly one `<Graph>` in the
   document; a value matching more than one Graph renders the model invalid.
4. Split companion formats for capabilities (`gcapx`) and bindings (`gbindx`) are **deferred
   indefinitely**; implementations MUST NOT require them.

# 2.0 — Core GDM (Graph Data Model)

The Graph Data Model describes the structure of a domain's data regardless of how it is stored.
A model MAY define multiple graphs; a single graph acts as the bounded context of one domain.

> §2.1 is intentionally unused in this revision: the document format that previously occupied it
> is layer-neutral and lives at §1.3. Numbering of the remaining §2 sections is preserved for
> stable cross-references.

## 2.2 — `<Graph ../>`

The Graph element encapsulates one bounded context: its structure *and* its behavior.

**Attributes**
- `Domain` — the bounded context represented by the Graph.
- `Alias` *(optional)* — used to avoid naming conflicts between Graphs.

**Rules**
1. Graph elements MAY be defined more than once within a model (§1.3).
2. All Graph `Domain` values MUST be unique within a model.
3. A Graph `Alias`, when defined, MUST be unique within a model, and MUST NOT equal any other
   Graph's `Domain`.

## 2.3 — Structural elements

Structural elements fall into two categories:

1. **Graph identity primitives** — `Node` and `Edge`: identity, addressability, traversal.
2. **Runtime value contracts** — `Type` and its members: payload shape, type checking,
   validation, serialization.

### 2.3.1 — Type references

Wherever a type is consumed within a Graph (`Property`, `Item`, and — in the Capability Model —
`Parameter` and `Result`, which reuse this same triple), the reference uses uniform attributes:

| Attribute | Values | Meaning |
| --- | --- | --- |
| `Type` | a defined Type name | how the runtime interprets the value |
| `Cardinality` | `One` (default) \| `Many` | whether the position yields one or many results — **graph multiplicity, not container shape** |
| `IsNullable` | `true` \| `false` (default) | whether the value may be absent |

**Rules**
1. A type reference resolves within the declaring Graph. Cross-graph *type* references are not
   supported in this revision (cross-graph *node* references exist for edges, §2.3.3 rule 2).
2. Collection types (§2.3.4.4) describe *container shape* (list, set, map); `Cardinality`
   describes *result multiplicity*. A traversal can return `Many` employees without a named
   collection type, and a capability can return a dictionary with `Cardinality="One"`.

### 2.3.2 — `<Node ../>`

A Node represents one or more entities within a graph and acts as an **entity anchor**: a
graph-facing structural element responsible for identity, addressability, and traversal.

```xml
<Node Name="{node name}" Type="{entity type}" />
```

**Rules**
1. A Node MUST be bound to exactly one `Entity` Type.
2. No more than one Node MAY be bound to the same Entity Type.
3. Node names MUST be unique within a Graph.

> *Deferred:* binding a Node to multiple entity types (polymorphic anchors) is documented in the
> layering proposal and deliberately **not** adopted in this revision (Appendix B).

### 2.3.3 — `<Edge ../>`

An Edge is a **first-class relationship** between two Nodes. Relationships do not live on the
node or entity type; the edge owns the relationship semantics, including its multiplicity and
any relationship-scoped data.

```xml
<Edge Name="{name}" Source="{source node}" Target="{target node}" Cardinality="{One|Many}">
  <!-- optional edge-owned properties -->
  <Property Name="{name}" Type="{type}" />
</Edge>
```

**Attributes**
- `Name`, `Source`, `Target` — required.
- `Cardinality` — required: `One` or `Many` targets per source.
- `Inverse` *(optional)* — the name of the edge that represents the reverse traversal.
- `IsDerived` *(optional)* — the edge is a stable semantic shortcut over a multi-hop traversal.

**Rules**
1. `Source` MUST reference a defined Node in the current Graph.
2. `Target` MUST reference a defined Node in the current Graph, or a fully qualified Node in
   another Graph (`/#Domain={domain}/#Node={node}`).
3. More than one Edge MAY exist between the same two Nodes, including self-references.
4. An Edge signature — the tuple (`Name`, `Source`, `Target`) — MUST be unique within a Graph.
5. An Edge MAY declare `Property` children; these are **edge-owned fields** (e.g. an assignment
   timestamp) and follow all Property rules (§2.3.5).
6. `Inverse`, when declared, MUST name an Edge whose `Source`/`Target` mirror this Edge.

### 2.3.4 — `<Type ../>`

A Type defines the **runtime contract** for values and payloads: shape, type checking,
validation, coercion, and serialization. Nodes and Edges own graph semantics; Types own value
and payload semantics.

```xml
<Type Name="{type name}" Kind="{Scalar|Enum|Complex|Collection|Entity|Directive}" />
```

**Rules**
1. Types MUST control serialization and type checking for their values.
2. Type names MUST be unique within a Graph. (Earlier drafts left the uniqueness scope
   ambiguous; per-Graph scoping is safe because type references never cross graphs —
   §2.3.1 rule 1. Recorded in Appendix A.)
3. A Type MUST NOT define graph identity or graph relationships.

#### 2.3.4.1 — Scalar Type

A Scalar represents a single, indivisible value derived from a primitive type
(`Integer`, `Float`, `String`, `Boolean`).

```xml
<Type Name="EmployeeId" Kind="Scalar">
  <PrimitiveType Type="String" />
</Type>
```

*[Owned by S-02 [O01.01.01.02]: full scalar rules — `<PrimitiveType>`/`<Format>` contracts,
constraints, coercion, and the default query capability of each scalar kind (§2.3.6.2).]*

#### 2.3.4.2 — Enum Type

*[Owned by S-02 [O01.01.01.02]: `<Member Name Value>` rules.]*

#### 2.3.4.3 — Complex Type

A Complex Type is a structured runtime contract composed of properties and functions: reusable
value objects for nested payloads, policy contracts, and other non-entity structures.

*[Owned by S-02 [O01.01.01.02]: completion of Complex rules and `<Function>` signatures.]*

#### 2.3.4.4 — Collection Type

Collection types exist for **implementation-significant container shapes** (list, set, map,
keyed collections) — never as a stand-in for result multiplicity (§2.3.1).

*[Owned by S-02 [O01.01.01.02]: `<Item>`/`<Key>`/`<Value>` rules and container semantics.]*

#### 2.3.4.5 — Entity Type

An Entity Type is a Complex Type with identity: it declares one or more `<Key>` members and is
the only Type kind a Node may bind to.

*[Owned by S-02 [O01.01.01.02]: `<Key>` rules, composite keys, key immutability.]*

#### 2.3.4.6 — Directive Type

A Directive Type declares an annotation contract (§1.1.2):

```xml
<Type Name="Authorize" Kind="Directive">
  <Usage On="Property|Function" />
  <Property Name="Roles" Type="String" Cardinality="Many" />
</Type>
```

Usage on an element:

```xml
<Property Name="EmployeeId" Type="EmployeeId">
  <Directive Name="Authorize">
    <Assign Property="Roles" Value="hr.read" />
  </Directive>
</Property>
```

**Rules**
1. A Directive Type MUST declare exactly one `<Usage On="...">` element; `On` is a
   `|`-separated list drawn from: `Graph`, `Node`, `Edge`, `Type`, `Property`, `Function`,
   `Parameter`, `Operation`. (`Parameter` and `Operation` are Capability-Model constructs, §3 —
   named here only as target vocabulary, mirroring the forward reference in §2.3.1.)
2. Directive names MUST NOT begin with `$.` (§1.1.2).
3. Directives MUST NOT grant or restrict query capability; capability is exclusively the domain
   of Policies.
4. A `<Directive Name="...">` application MUST target an element permitted by the Directive
   Type's `Usage`, and assigns values with `<Assign Property="{name}" Value="{value}" />`
   children.

*[Owned by G-08 [O01.01.02.08]: the full runtime contract — `Assign` typing/coercion,
repeatability, reserved directives.]*

### 2.3.5 — `<Property ../>`

The Property element defines a named member of a Complex Type, Entity Type, or Edge.

```xml
<Property Name="{name}" Type="{type}" IsReadOnly="{true|false}" IsNullable="{true|false}"
          Cardinality="{One|Many}">
  <Policy Name="{policy name}">
    <Operator Name="{operator}" />
    <Function Name="{function}" />
    <Option Name="{option}" Value="{value}" />
  </Policy>
</Property>
```

**Rules**
1. Property names MUST be unique within the declaring element.
2. A Property `Type` MUST reference a defined Type using the §2.3.1 triple.
3. A Property MAY declare zero or more `Policy` children (§2.3.6) and zero or more `Directive`
   children (§2.3.4.6).
4. A Property MAY be read-only or nullable as declared by its attributes.

### 2.3.6 — Property-hosted `<Policy ../>`

When declared beneath a `Property`, a Policy defines the **maximum query capability** of that
member, regardless of which operation exposes it.

**Rules**
1. Policy names MUST be unique within the containing Property.
2. A Property-hosted Policy MUST NOT declare a `Type` attribute.
3. A Property-hosted Policy MAY declare zero or more `Operator`, `Function`, or `Option`
   children.
4. Property policies MUST remain data-source agnostic and describe only portable query behavior.

*[Owned by S-03 [O01.01.01.03]: the operator and function name vocabularies. Owned by
S-02 [O01.01.01.02]: `Option` names and per-type defaults.]*

#### 2.3.6.1 — Reserved Property Policies

Reserved policy names beneath a `Property`: **`$.filter`**, **`$.sort`**, **`$.project`**.
(`$.page` is *not* a Property policy — paging is an operation-scope concern, §3.3.2.)

**Rules**
1. `$.filter` bounds the operators and functions usable when the member is referenced in
   `.filter()`.
2. `$.sort` bounds whether and how the member participates in `.sort()`.
3. `$.project` bounds whether the member participates in `.project()`.
4. A Property policy MAY narrow the behavior otherwise implied by the member's Type.

#### 2.3.6.2 — Effective member capability

The **effective capability** of a member for a reserved concern (`$.filter`, `$.sort`,
`$.project`) is:

- the Property-hosted policy for that concern, when declared; otherwise
- the **default capability of the member's Type** (defaults per type kind are pinned by S-02;
  until then they are implementation-defined).

A Property-hosted policy therefore *narrows* — its absence does **not** make the member
unusable; keyword availability is gated exclusively at the operation level (§3.3.3).

# 3.0 — Capability Model

Capability (behavioral) elements define executable intent over a Graph. They are declared inside
`<Graph>` alongside the structure they operate on, and remain transport-neutral: nothing in this
section implies a transport method, route, or parameter location.

## 3.1 — Operations

Operations come in two kinds — `Query` (data retrieval) and `Command` (state change) — and two
scopes:

1. **Root Operations** — bound to a `Node`; the entry points into a Graph.
2. **Resolver Operations** — bound to an `Edge`; executed when `.edge(...)` is invoked within an
   OGraph query.

An operation's **call signature** is the tuple (operation kind, bound scope, set of non-reserved
parameter names).

### 3.1.1 — `<Query ../>`

```xml
<Query Name="{name}" Node="{node}" Edge="{edge}">
  <!-- Parameters -->
  <Result Type="{type}" Cardinality="{One|Many}" />
</Query>
```

**Rules**
1. A Query MUST declare exactly one of `Node` or `Edge`.
2. `Node`, when present, MUST reference a defined Node in the current Graph; the Query is a Root
   Operation.
3. `Edge`, when present, MUST identify a single defined Edge in the current Graph; the Query is
   a Resolver Operation.
4. Query names MUST be unique within a Graph.
5. No two Queries bound to the same `Node` or `Edge` MAY expose the same call signature.
6. A Query MUST NOT cause an observable state change. (Protocol bindings map this guarantee onto
   transport-level safety, §4.)

### 3.1.2 — `<Command ../>`

```xml
<Command Name="{name}" Node="{node}" Edge="{edge}">
  <!-- Parameters -->
  <Result Type="{type}" Cardinality="{One|Many}" />
</Command>
```

**Rules**

Rules 1–5 of §3.1.1 apply identically to Commands. Commands represent state change and carry no
safety guarantee.

> *Deferred:* the layering proposal's `Action` taxonomy on Commands
> (`Create|Update|Patch|Delete|Custom`) is not adopted in this revision (Appendix B).

### 3.1.3 — `<Result ../>`

Every operation declares its outcome with a `Result` element using the type-reference triple
(§2.3.1):

```xml
<Result Type="Employee" Cardinality="Many" />
```

**Rules**
1. An operation MUST declare exactly one `Result`.
2. `Cardinality` expresses result multiplicity; a `Result` MUST NOT use a Collection Type solely
   to express multiplicity (use `Cardinality="Many"`), but MAY reference a Collection Type when
   the container shape itself is the contract (e.g. a keyed dictionary).
3. The element form `<ReturnType Type="..."/>` from earlier drafts is superseded by `Result`.

> *Deferred:* a Resolver Operation's ability to expose the edge-owned payload alongside the
> target node payload is not specified in this revision (Appendix B; revisit under S-04/G-03).

## 3.2 — `<Parameter ../>`

Parameters define arguments accepted by a Query or Command — ordinary operation arguments or
reserved protocol arguments.

```xml
<Parameter Name="{name}" Type="{type}" IsRequired="{true|false}" Role="{role}" />
```

**Rules**
1. Parameter names MUST be unique within the containing operation.
2. Reserved parameter names MUST begin with `$.`. This revision reserves exactly one parameter
   name: **`$.query`** (§3.3). Future reserved parameters will be registered in this section.
3. Non-reserved parameters participate in call-signature resolution; reserved parameters do not.
4. Only the reserved `$.query` parameter MAY declare `Policy` children (§3.3.2). (Earlier
   drafts permitted policies on any parameter; recorded in Appendix A.)
5. A non-reserved Parameter declares a semantic `Role`: `Key` (addresses a specific entity),
   `Argument` (ordinary operation argument — the default), `Input` (a payload object carrying
   state for a Command), `Context` (ambient/environmental value), or `Cursor` (pagination
   continuation token). Roles are semantic, never transport locations.

## 3.3 — The reserved `$.query` parameter

### 3.3.1 — Declaration

The `$.query` parameter is reserved for OGraph query text and query-control policies.

**Rules**
1. `$.query` SHOULD be declared with a string-compatible scalar type.
2. `$.query` MAY declare one or more `Policy` children.
3. An operation that supports the OGraph query keywords `.filter()`, `.sort()`, `.project()`, or
   `.page()` MUST expose them through policies declared on `$.query`.

### 3.3.2 — Reserved Query Policies

Reserved policy names beneath `$.query`: **`$.filter`**, **`$.sort`**, **`$.project`**,
**`$.page`**.

```xml
<Parameter Name="$.query" Type="String">
  <Policy Name="$.filter"  Type="EmployeeFilter" />
  <Policy Name="$.sort"    Type="EmployeeSort" />
  <Policy Name="$.project" Type="EmployeeProjection" />
  <Policy Name="$.page"    Type="PagePolicy" />
</Parameter>
```

**Rules**
1. Policy names MUST be unique within the containing `$.query` parameter.
2. A `$.query`-hosted Policy declares a `Type` that MUST reference a defined `Complex` Type
   whose members describe **query-visible members** — never storage-specific paths.
3. Policy types MAY include nested complex members to describe relationship-scoped members, and
   MUST NOT require explicit edge-path annotations to expose them.
4. `$.page` is operation-scope only: its policy Type describes the paging arguments (e.g. skip,
   take, cursor members), and no member-level counterpart exists (§2.3.6.1).
5. Implementations MAY define additional custom policies beneath `$.query`; custom policy names
   SHOULD use a namespace or vendor prefix.

### 3.3.3 — Keyword gating and exposure resolution

Keyword availability and member exposure resolve as follows:

1. **Gating.** If `$.query` (or a reserved policy beneath it) is not declared on an operation,
   the corresponding query keyword is **unavailable** in that operation scope.
2. **Member exposure.** For `$.filter`, `$.sort`, and `$.project`: a member is exposed to the
   keyword iff a corresponding member appears in the operation policy's Complex Type. Members
   absent from the policy type are not addressable by that keyword in that operation scope.
3. **Capability bounding (the intersection rule).** Each exposed member's usable operators,
   functions, and options are its **effective member capability** (§2.3.6.2) — the operation
   level selects *which* members are exposed; the member level (declared policy or type
   default) bounds *what can be done* with them.
4. **No broadening.** An operation-level policy MUST NOT expose capability beyond a member's
   effective capability — e.g. exposing a member to `.sort()` whose `$.sort` effective
   capability forbids sorting. A model that does so is **invalid**.
5. Exposure resolution MUST be relative to the `Node` or `Edge` bound by the containing
   operation.

## 3.4 — Event

*[Owned by S-11 [O01.01.01.11]: event and subscription modeling (pub/sub).]*

## 3.5 — Subscription

*[Owned by S-11 [O01.01.01.11].]*

# 4.0 — Protocol Bindings

Bindings map capabilities onto HTTP. They live in `<Bindings>` sections (§1.3) so that the Core
GDM and Capability Model stay transport-neutral. The §3.1.1 rule-6 safety guarantee corresponds
at this layer to *safe* in the RFC 9110 sense; bindings for Queries MUST use safe, idempotent
methods.

```xml
<Bindings Graph="Hrm">
  <Binding Operation="GetEmployees" Method="QUERY" Route="/hrm/employees">
    <Map Parameter="$.query" Source="Body" MediaType="application/vnd.ograph.query" />
  </Binding>
</Bindings>
```

**Conventions**
1. All routes MUST begin with a literal segment matching the Graph's `Domain` or `Alias`.
2. For Root Operations, the literal segment following the Domain/Alias segment MUST match the
   target Node. Route-segment matching is case-insensitive; lower-case route casing is
   RECOMMENDED (the `Hrm`/`/hrm` pairing above is conformant).
3. Resolver Operations MAY execute purely as part of query resolution and do not require
   independent public routes unless explicitly bound.

*[Owned by S-05 [O01.01.01.05]: the complete HTTP binding specification — the `<Binding>` and
`<Map>` element contracts; routing rules; the RFC 10008 QUERY method as the primary read binding
with the query in the request body; media types; the 400/415/422 status taxonomy; `Accept-Query`
advertisement; caching and conditional semantics; `Location`/`Content-Location`; and the optional
`GET ?query=` fallback profile. Command query-control placement is owned by S-07 [O01.01.01.07].]*

# 5.0 — Query Language

The OGraph query language addresses a Root Operation and composes keyword clauses. The canonical
entry keyword is `node(...)` (alias `n(...)`); `vertex(...)`/`v(...)` are accepted synonyms
(§1.1.1).

The complete surface syntax is defined by the committed grammar at
[`grammar/ograph-query.ebnf`](grammar/ograph-query.ebnf), which is **normative**: it governs what
a conforming parser accepts and rejects, and it seeds the conformance suite (N-03). This section
defines the semantics that the grammar's syntax carries — keyword availability, the operator and
function vocabulary, clause evaluation order, paging modes, and edge traversal and scoping. Where
this prose and the grammar file disagree on surface form, the grammar file governs; where they
disagree on meaning, this section governs.

## 5.1 — Keywords

| Keyword | Availability rule |
| --- | --- |
| `node(...)` † | selects a Node-bound Root Operation as the starting scope |
| `.filter()` | only when the operation exposes `$.query` with a `$.filter` policy |
| `.sort()` | only when the operation exposes `$.query` with a `$.sort` policy |
| `.page()` | only when the operation exposes `$.query` with a `$.page` policy |
| `.project()` | only when the operation exposes `$.query` with a `$.project` policy |
| `.edge()` | resolves against Resolver Operations available within the current scope |

† Entry keyword, not a chained clause. Aliases `n(...)`, `vertex(...)`, `v(...)` share this row.

**Rules**
1. `.edge()` resolution: if more than one Resolver Operation in the same scope exposes the same
   call signature, the model is invalid.
2. Keyword availability is validated against the capability model (§3.3.3) **before** execution.
3. Within a scope, clauses are evaluated in the canonical order **filter → sort → page →
   project**, regardless of their textual order in the query; the full evaluation-order semantics
   are specified in §5.4.

## 5.2 — Grammar

The surface syntax of every construct named in this section — the entry keyword, the clause
chain, filter expressions, literals, variables (`$var`), comments (`#`), and the clause bodies —
is defined by [`grammar/ograph-query.ebnf`](grammar/ograph-query.ebnf). That file is the
**normative grammar** delivered by S-03 [O01.01.01.03] (grammar) and S-04 [O01.01.01.04]
(edge-traversal syntax and scoping); it supersedes the informal paging fragment of earlier drafts.
Its RECONCILIATION section records the design-sketch constructs
(`.designing/query.ograph`) that are deliberately rejected and their canonical replacements.

A clause body is a brace-delimited block. `.filter({ … })`, `.sort({ … })`, `.page({ … })`, and
`.project({ … })` each carry their body in a `{ … }` block; a body presented without its block is
invalid (diagnostic `G0003`). Nested selection sets, nested sort-key sets, and the predicate body
of an `any`/`all` lambda are likewise `{ … }` blocks.

## 5.3 — Operator and function vocabulary

This subsection defines the operator and function **names** that §2.3.6 defers to S-03. These are
the portable query operations; a Property-hosted `$.filter`/`$.sort` policy (§2.3.6) selects which
of them a member permits, and an operation-level policy (§3.3.3) selects which members are exposed.
A model MUST NOT admit an operator or function name outside this vocabulary unless it is an
implementation extension carrying a vendor prefix.

### 5.3.1 — Operators

| Class | Name | Spelling | Arity | Notes |
| --- | --- | --- | --- | --- |
| Comparison | Equal | `eq` | binary | non-associative |
| Comparison | Not equal | `neq` | binary | non-associative |
| Comparison | Greater than | `gt` | binary | non-associative |
| Comparison | Greater than or equal | `gte` | binary | non-associative |
| Comparison | Less than | `lt` | binary | non-associative |
| Comparison | Less than or equal | `lte` | binary | non-associative |
| Logical | And | `and` | binary | left-associative |
| Logical | Or | `or` | binary | left-associative |
| Arithmetic | Add | `+` | binary | left-associative |
| Arithmetic | Subtract | `-` | binary | left-associative |
| Arithmetic | Multiply | `*` | binary | left-associative |
| Arithmetic | Divide | `/` | binary | left-associative |

**Precedence**, tightest to loosest: grouping `( )` and function call → multiplicative `* /` →
additive `+ -` → comparison (`eq neq gt gte lt lte`) → `and` → `or`. Comparison operators do not
chain (`a lt b lt c` is invalid); use grouping and `and`. The layered productions in the grammar
(`or-expr`, `and-expr`, `comparison-expr`, `additive-expr`, `multiplicative-expr`) encode this
precedence, so a conforming parser needs no separate precedence table at runtime.

### 5.3.2 — Functions

Function names are matched case-insensitively. `contains` and `length` are polymorphic over string
and collection operands.

| Family | Name | Signature (informal) | Result |
| --- | --- | --- | --- |
| String | `startsWith` | `(string, string [, ignoreCase])` | Boolean |
| String | `endsWith` | `(string, string [, ignoreCase])` | Boolean |
| String | `contains` | `(string, string)` | Boolean |
| String | `concat` | `(string, string, …)` | String |
| String | `substring` | `(string, start [, length])` | String |
| String | `padLeft` | `(string, width [, char])` | String |
| String | `padRight` | `(string, width [, char])` | String |
| String | `trim` | `(string)` | String |
| String | `trimLeft` | `(string)` | String |
| String | `trimRight` | `(string)` | String |
| Collection | `any` | `(collection, predicate)` | Boolean |
| Collection | `all` | `(collection, predicate)` | Boolean |
| Collection | `contains` | `(collection, value)` | Boolean |
| Collection / Numeric | `length` | `(string | collection)` | Integer |

`any` and `all` are **lambda functions**: their second argument is a predicate evaluated in the
element scope of the collection named by the first argument. The predicate body is a `{ … }` block
(which MAY be empty, denoting the always-true predicate) or a bare predicate expression. Nesting is
permitted: a lambda predicate MAY reference members of the element and MAY itself invoke `any`/`all`
over a nested collection.

The `startsWith`/`endsWith` optional third argument is a Boolean case-insensitivity flag. A member
function used inside `.project()` (e.g. `fullName(FullNameFormat.FLM)`) shares this call syntax; its
availability is bounded by the member's `$.project` capability (§2.3.6.1) and its enum-qualified
argument (`FullNameFormat.FLM`) is an ordinary member path.

## 5.4 — Clause evaluation-order semantics

Within a single scope, clauses are evaluated in the canonical order **filter → sort → page →
project**, regardless of their textual order in the query text and regardless of repetition.

**Rules**
1. **Canonical order.** A scope's clauses evaluate as: `filter` narrows the candidate set; `sort`
   orders the survivors; `page` selects a window over the ordered survivors; `project` shapes the
   selected results. Textual order is cosmetic and MUST NOT change results.
2. **At most one of each per scope.** A scope MUST NOT declare two `filter`, two `sort`, two
   `page`, or two `project` clauses. A model or query that does so is invalid.
3. **Paging follows ordering.** Because `page` selects a window, a `page` clause without a `sort`
   clause in the same scope yields an implementation-defined but stable order; a conforming
   implementation SHOULD document its default. Cursor paging (§5.5) MUST resolve against the same
   ordering that produced the cursor.
4. **Projection is last.** Members removed by `project` are still available to `filter`, `sort`,
   and `page` in the same scope; a member referenced by those clauses need not appear in
   `project`.
5. **Per-scope application.** Evaluation order applies independently within each edge scope
   (§5.6). A child scope's clauses evaluate over the results the parent scope yields for each
   parent element.
6. **Availability precedes evaluation.** Keyword availability is validated against the capability
   model (§3.3.3) before any clause evaluates; an unavailable keyword is a validation error, not a
   runtime one.

## 5.5 — Paging forms

Paging is expressed by the `page` clause; both modes carry their arguments in the `{ … }` block.

**Offset mode** selects a window by position. `skip` and `take` are order-independent, and either
MAY be omitted (`skip` defaults to `0`; an absent `take` yields an implementation-defined page
size).

```ograph
.page({
  skip 0
  take 25
})
```

**Cursor mode** continues from an opaque continuation token. `token` carries the cursor; it MAY be
combined with `take` to bound the page size and MAY be combined with `skip` to apply an additional
starting offset relative to the cursor position. The grammar's `page-set` admits `skip`, `take`,
and `token` in any combination within one `page` clause, and the golden parser test
(`CompleteParseSuccessful`) exercises `page({ skip 0 take 25 token '' })` accordingly.

```ograph
.page({
  take 25
  token '<opaque-cursor>'
})
```

Each `skip`/`take` value is an integer literal or a variable (`$take`); each `token` value is a
string literal or a variable. A cursor is produced by a prior page of the same scope and ordering
(§5.4 rule 3); its internal structure is opaque to the client and is defined by the response
envelope owner (S-06).

## 5.6 — Edge traversal and scoping *(S-04 [O01.01.01.04])*

### 5.6.1 — Decision: the path form is canonical

The design sketch diverged between an **arrow form** `.edge(e-[has]->addresses:a)` and a **path
form** `.edge(node/edge)` / `.edge(node:/edge)`. **The path form is canonical; the arrow form is
rejected.** Rationale:

1. **Lexability.** The arrow form is not tokenizable by the committed lexer: `[` and `]` are not
   recognized tokens, so `-[has]->` cannot be lexed. The path form uses only `/`, identifiers, and
   the `as` keyword, all of which the lexer and parser already accept.
2. **Executable ground truth.** Every passing edge test uses the path form
   (`.edge(companies/addresses)`, `.edge(department as employeeDepartment)`,
   `.edge(jobs/tasks/workItems)`), asserting `document.Errors` is empty.
3. **GDM alignment (S-01).** An Edge is a first-class relationship addressed by its `Name` from a
   `Source` Node to a `Target` Node (§2.3.3). A `/`-separated selector is exactly a directed-path
   segment sequence over that model (walk/trail/path, `spec-notes.md`); the arrow form's inline
   relationship label duplicates the Edge `Name` the model already owns.

The `:/` and bare-`:` scope spellings and the colon alias `:a` are folded onto the single path
separator `/` and the single alias keyword `as`. The grammar's RECONCILIATION notes R2–R3 record
the surface mapping.

### 5.6.2 — Canonical edge syntax

```ograph
.edge( edge-selector [ , argument ] )
```

- **`edge-selector`** is `edge-path [ as alias ]`, where `edge-path` is one or more identifiers
  separated by `/` (`edge`, or `scope/edge`, or `scope/edge/edge`).
- **`alias`** (`as name`) names the **target scope** the traversal produces, so deeper clauses can
  re-anchor to it. The target scope **always** carries an **implicit alias** — the final
  `edge-path` segment (the Edge name) — whether or not an explicit `as` alias is given; when an `as`
  alias is present it is an additional name for the same scope. Either name may head a qualified
  path (§5.6.3 rule 2).
- **`argument`** is an optional single-entity key applied to the traversal target (Role = `Key`,
  §3.2 rule 5).

### 5.6.3 — Scope resolution

Every query has a **scope stack**. The entry keyword establishes the **root scope**, bound to the
entry Node; the entry Node's `Name` is the root scope's **implicit alias**, and an explicit `as`
name, when present, is an additional alias for the same scope. Each `.edge(...)` resolves against
the scope stack and pushes a **child scope** bound to the resolved Edge's `Target` Node.

**Rules**
1. **Single-segment selector.** `.edge(name)` resolves `name` as an Edge available from the
   **current (innermost)** scope's Node (a Resolver Operation, §3.1); the Edge's `Target` becomes
   the new innermost scope.
2. **Qualified path.** `.edge(a/b/c)` resolves left-to-right: `a` MUST name an **in-scope scope** —
   matched against that scope's **implicit alias** (the entry Node's `Name` for the root scope, or
   the Edge `Name` that produced an enclosing edge scope per §5.6.2) or its explicit `as` alias —
   and each following segment is an Edge `Name` walked from the prior segment's target. The final
   segment's `Target` becomes the new innermost scope. A qualified path is how a clause
   **re-anchors** to an outer scope rather than the innermost one. Thus `.edge(companies as
   employeeCompanies)` followed by `.edge(companies/addresses)` re-anchors via the Edge-name
   implicit alias `companies`, and an unaliased root `v(providers)` re-anchors via its Node-name
   implicit alias in `.edge(providers/languages)`.
3. **Scope binding of chained clauses.** `.filter()`, `.sort()`, `.page()`, `.project()`, and a
   further `.edge()` that follow an `.edge(...)` bind to that edge's **target scope** (the innermost
   scope) until a subsequent `.edge(...)` re-anchors or pushes a new scope. Textual indentation is
   cosmetic; binding is determined solely by the scope stack and the selector, not by whitespace.
4. **Alias uniqueness.** Within one root query, an `as` alias MUST be unique across live scopes; a
   selector segment that matches no in-scope alias and no Edge in the current scope is a resolution
   error.
5. **Resolver ambiguity.** If more than one Resolver Operation in the resolved scope exposes the
   same call signature for the selected Edge, the model is invalid (§5.1 rule 1).
6. **Per-scope semantics.** Each scope applies the §5.4 evaluation order independently; a child
   scope's clauses evaluate over the parent scope's yielded results for each parent element, and
   partial-failure capture across resolver scopes is defined by edge execution (V-06) and the
   response envelope (S-06).

# 6.0 — Communication

*[Owned by S-05 [O01.01.01.05] (server/binding semantics, including the client-facing behaviors
a conforming client depends on: `Accept-Query` discovery, `Location`/`Content-Location`
re-fetch) and S-06 [O01.01.01.06] (the response envelope: `$nodes`/`$edges` composition,
per-edge `$status`/`$errors` partial-failure semantics, and paging metadata, with a committed
JSON Schema and golden fixtures). Client conformance is defined by consuming these two
contracts; the client implementation itself is roadmap L-01 [O01.01.06.01].]*

---

## Appendix A — Supersessions in this revision

| Superseded construct | Replacement | Where |
| --- | --- | --- |
| `Vertex` as schema/runtime term | `Node` (Vertex = query-text alias only) | §1.1.1 |
| `<Directive Type="$.filter">` capability grants | `<Policy Name="$.filter">` | §1.1.2, §2.3.6 |
| `<ReturnType Type="X[]"/>` / collection-as-multiplicity | `<Result Type="X" Cardinality="Many"/>` | §3.1.3 |
| Edges without multiplicity | required `Cardinality` on `<Edge>` | §2.3.3 |
| Separate `gcapx`/`gbindx` documents | single-file `gdmx` with `<Bindings>` sections | §1.3 |
| Standalone `<Gdmxb>` binding document | `<Bindings>` inside `gdmx` | §1.3, §4.0 |
| Policies on arbitrary Parameters | Policies only on `$.query` | §3.2 rule 4 |
| "All literal segments MUST match a node name" (old routing rule) | conventions 1–2 of §4.0 (Domain/Alias first, Node second) | §4.0 |
| Unscoped "All Type Names MUST be unique" | uniqueness scoped per Graph | §2.3.4 rule 2 |
| "Property falls back to implementation defaults" as a *capability* statement | type-default **effective capability**; gating is operation-level only | §2.3.6.2, §3.3.3 |

## Appendix B — Recorded deltas from the ratified layering proposal

| Proposal construct | Disposition |
| --- | --- |
| Multi-entity Node bindings (`EntityTypeRef`, `DefaultEntityType`) | **Deferred** — single Entity Type per Node retained (§2.3.2) |
| Split `gcapx`/`gbindx` document formats | **Deferred indefinitely** — single-file `gdmx` adopted (§1.3) |
| Command `Action` taxonomy (`Create\|Update\|Patch\|Delete\|Custom`) | **Deferred** — commands are named capabilities without an action attribute (§3.1.2) |
| Edge-scoped queries exposing the edge payload alongside the target payload | **Deferred** — revisit under S-04/G-03 (§3.1.3) |
| Semantic parameter roles (`Key\|Argument\|Input\|Context\|Cursor`) | **Adopted** (§3.2 rule 5) |
| Field capability facets (`Selectable`/`Filterable`/`Sortable` attributes) | **Not adopted** — expressed via Property-hosted policies instead (§2.3.6) |
