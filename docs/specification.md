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

The primary read binding is the HTTP **QUERY** method defined by
[RFC 10008](https://www.rfc-editor.org/rfc/rfc10008.html): a request method that is *safe* and
*idempotent* (RFC 9110 [§9.2.1](https://www.rfc-editor.org/rfc/rfc9110#section-9.2.1) /
[§9.2.2](https://www.rfc-editor.org/rfc/rfc9110#section-9.2.2)), yet — unlike GET — carries
request content. This resolves the URL-length and percent-encoding limits of `?query=` and gives
body-carried queries a *standardized*, cacheable model that POST never had (RFC 10008
[§1](https://www.rfc-editor.org/rfc/rfc10008#section-1)).

## 4.1 — The `<Binding>` and `<Map>` elements

A `<Bindings>` section contains one `<Binding>` per bound operation; each `<Binding>` contains
zero or more `<Map>` children that route the operation's parameters to transport locations.

```xml
<Binding Operation="{query-or-command name}" Method="{HTTP method}" Route="{path template}">
  <Map Parameter="{parameter name}" Source="{Body|Path|Query|Header}"
       MediaType="{media type}" Name="{transport name}" />
</Binding>
```

**`<Binding>` attributes**
- `Operation` — required; MUST name exactly one `Query` or `Command` in the Graph identified by
  the enclosing `<Bindings Graph="…">` (§1.3 rule 3).
- `Method` — required; the HTTP method token. For a `Query` it MUST be a safe, idempotent method:
  **`QUERY`** (the primary read binding, §4.3) or **`GET`** (the OPTIONAL fallback, §4.10). For a
  `Command` it is one of `POST`, `PUT`, `PATCH`, `DELETE` (§4.11).
- `Route` — required; a path template rooted at the Graph (§4.2).

**`<Map>` attributes**
- `Parameter` — required; a parameter of the bound operation, including the reserved `$.query`
  (§3.3).
- `Source` — required; the transport location the value is drawn from: `Body` (request content),
  `Path` (a route template segment), `Query` (a URI query-component parameter), or `Header` (a
  request header field). `Source` is the *transport* location; it is distinct from the parameter's
  semantic `Role` (§3.2 rule 5).
- `MediaType` — OPTIONAL; meaningful only when `Source="Body"`, it fixes the media type of the
  mapped content (§4.4).
- `Name` — OPTIONAL; the transport-side name when it differs from `Parameter` (e.g. the
  `query` URI-parameter name in the GET fallback). Defaults to `Parameter`.

**Rules**
1. A `<Binding>`'s (`Method`, `Route`) pair MUST be unique within a model.
2. At most one `<Map>` per `<Binding>` MAY declare `Source="Body"`.
3. For a `QUERY` read binding, the reserved `$.query` parameter MUST be mapped to
   `Source="Body"` with `MediaType="application/vnd.ograph.query"` (§4.3, §4.4).
4. A `<Map>` whose `Parameter` is `$.query` MUST NOT use `Source="Path"`.
5. Every `Path` `<Map>` MUST correspond to a template segment present in `Route`, and every
   required non-reserved parameter of the operation MUST be mapped by exactly one `<Map>`.

## 4.2 — Routing

**Conventions**
1. All routes MUST begin with a literal segment matching the Graph's `Domain` or `Alias`.
2. For Root Operations, the literal segment following the Domain/Alias segment MUST match the
   target Node. Route-segment matching is case-insensitive; lower-case route casing is
   RECOMMENDED (the `Hrm`/`/hrm` pairing above is conformant).
3. Resolver Operations MAY execute purely as part of query resolution and do not require
   independent public routes unless explicitly bound. When a Resolver Operation *is* independently
   bound, its route SHOULD nest beneath the source Node's route (e.g.
   `/hrm/employees/{id}/addresses`), and it is subject to the same method and media-type rules as
   a Root read binding.
4. Route templates denote path parameters with `{name}` segments; each corresponds to a `Path`
   `<Map>` (§4.1 rule 5). The bound Node segment (convention 2) is a literal, not a parameter.

## 4.3 — The QUERY read binding

**Rules**
1. A Query's primary binding MUST use `Method="QUERY"` with the OGraph query text carried as the
   request content, mapped from the reserved `$.query` parameter (§4.1 rule 3). The request
   content is the *complete* query text; the URI carries no query clause.
2. A server that binds a Query to `QUERY` MUST treat the method as safe and idempotent (RFC 10008
   [§2](https://www.rfc-editor.org/rfc/rfc10008#section-2)): the request MUST NOT cause an
   observable state change, upholding the §3.1.1 rule-6 guarantee at the transport layer.
3. Per RFC 10008 [§2](https://www.rfc-editor.org/rfc/rfc10008#section-2), a server MUST fail the
   request when the `Content-Type` field is missing or is inconsistent with the request content;
   the OGraph mapping of these failures onto status codes is §4.5.
4. A successful QUERY response returns the response envelope (§6) with media type
   `application/vnd.ograph+json` and the status taxonomy of §4.5. The *selected representation*
   of a QUERY response is the representation a GET on the equivalent resource (RFC 10008
   [§2.2](https://www.rfc-editor.org/rfc/rfc10008#section-2.2)) would return; this equivalence is
   what makes caching (§4.7) and re-fetch (§4.8) well-defined.

## 4.4 — Media types and registration path

Two media types are defined:

| Direction | Media type | Notes |
| --- | --- | --- |
| Request content (the query) | `application/vnd.ograph.query` | Character encoding is **fixed to UTF-8 by definition**; the type takes no `charset` parameter. |
| Response envelope | `application/vnd.ograph+json` | A JSON-structured type using the `+json` structured-syntax suffix ([RFC 6839 §4.2](https://www.rfc-editor.org/rfc/rfc6839#section-4.2), [RFC 8259](https://www.rfc-editor.org/rfc/rfc8259)); its structure is §6 and its schema is [`schemas/ograph-response.schema.json`](schemas/ograph-response.schema.json). |

**Registration path (decided).** Both names are registered in the **vendor tree** (the `vnd.`
facet) per [RFC 6838 §3.2](https://www.rfc-editor.org/rfc/rfc6838#section-3.2). Standards-tree
names such as `application/ograph-query` require IETF review or another standards action and
[RFC 6838 §3.1](https://www.rfc-editor.org/rfc/rfc6838#section-3.1) forbids claiming them
unilaterally; the vendor tree is registerable now by the owner of the `ograph` name and keeps the
`vnd.ograph.*` identifiers stable. A future migration to the standards tree remains open and, if
taken, would register the standards-tree names as additional aliases rather than renaming these.
Implementations MUST NOT depend on any name outside this section.

**Rules**
1. A conforming server MUST accept `application/vnd.ograph.query` as a query media type on its
   QUERY read bindings and MUST emit `application/vnd.ograph+json` for the envelope.
2. A server MUST NOT require a `charset` parameter on `application/vnd.ograph.query`; if one is
   present it MUST be `utf-8`, and any other value is an unsupported query media type (§4.5).

## 4.5 — Status taxonomy

Request-body outcomes map onto status codes exactly as follows (RFC 10008
[§2.1](https://www.rfc-editor.org/rfc/rfc10008#section-2.1) + RFC 9110
[§15](https://www.rfc-editor.org/rfc/rfc9110#section-15)):

| Condition | Status |
| --- | --- |
| `Content-Type` is **missing** | **400** Bad Request |
| `Content-Type` is present but the **query media type is unsupported** | **415** Unsupported Media Type |
| The media type is supported but the **body is inconsistent with it** (e.g. not well-formed query text) | **400** Bad Request |
| The query is well-formed but **fails semantic or policy validation** | **422** Unprocessable Content, with the diagnostics payload (§6.4) |

**Rules**
1. A server MUST apply exactly the four mappings above; in particular it MUST NOT return 415 for a
   supported-but-malformed body (that is 400) and MUST NOT return 400 for a well-formed
   policy-violating query (that is 422).
2. A `415` response SHOULD advertise the accepted query media types via `Accept-Query` (§4.6) so
   the client can retry.
3. Response-format negotiation is separate from the request-body taxonomy: when the request's
   `Accept` cannot be satisfied by `application/vnd.ograph+json`, the server MAY return **406**
   Not Acceptable (RFC 10008 [§2.1](https://www.rfc-editor.org/rfc/rfc10008#section-2.1)).
4. `400`, `415`, `422`, and `406` responses carry a response envelope (§6) whose `$status` mirrors
   the transport status and whose `$errors` describe the failure.

## 4.6 — `Accept-Query` discovery

Servers advertise QUERY support with the `Accept-Query` response header field (RFC 10008
[§3](https://www.rfc-editor.org/rfc/rfc10008#section-3)), an
[RFC 8941](https://www.rfc-editor.org/rfc/rfc8941) Structured Field **List** whose members are the
supported query media types.

**Rules**
1. `Accept-Query` MUST be emitted on responses from bound resources, including the response to
   `OPTIONS` (§4.9) and to `415` (§4.5 rule 2). Its members are query media types (§4.4).
2. OGraph advertises members **without media-type parameters**. Because
   `application/vnd.ograph.query` is UTF-8 by definition (§4.4) there is no `charset` to convey;
   the general RFC 10008 §3 allowance for parameters is intentionally unused by this profile.
3. The field value is **scoped to the path**: it applies to all URIs sharing the request path,
   and the URI query component is ignored (RFC 10008
   [§3](https://www.rfc-editor.org/rfc/rfc10008#section-3)). A client MUST use the most recently
   received fresh value for a given path.

Example: `Accept-Query: "application/vnd.ograph.query"`.

## 4.7 — Caching and conditional requests

QUERY responses are cacheable ([RFC 9111](https://www.rfc-editor.org/rfc/rfc9111)); RFC 10008
[§2.7](https://www.rfc-editor.org/rfc/rfc10008#section-2.7) binds *caches* to key a stored QUERY
response on the **request content plus related request metadata** (method, target URI, and the
`Vary`-selected headers), not on the URI alone. That obligation is the cache's; the **server's**
obligations at this layer are:

**Rules**
1. A cacheable QUERY response MUST carry a validator — a strong or weak `ETag`
   (RFC 9110 [§8.8.3](https://www.rfc-editor.org/rfc/rfc9110#section-8.8.3)) — and SHOULD carry a
   `Last-Modified` where meaningful, so conditional re-validation is possible.
2. A server MUST emit accurate freshness metadata (`Cache-Control`, and `Vary` for any request
   header that selects the representation).
3. Conditional headers on a QUERY (`If-None-Match`, `If-Modified-Since`, …) behave exactly as for
   a GET on the equivalent resource (RFC 10008
   [§2.6](https://www.rfc-editor.org/rfc/rfc10008#section-2.6)): when the selected representation
   is unchanged the server returns **304** Not Modified with no body and the current validator.
4. A cache MAY normalize semantically insignificant request-content differences when computing the
   cache key (RFC 10008 [§2.7](https://www.rfc-editor.org/rfc/rfc10008#section-2.7)); because an
   incorrect normalization can return a wrong result, a server that requires byte-exact keying
   MUST signal it (e.g. `Cache-Control: no-transform`). `no-transform` here is advisory to caches,
   and OGraph fixes the query encoding to UTF-8 (§4.4) to remove the most common normalization
   hazard.

## 4.8 — Re-fetch: `Location` and `Content-Location`

To let a client re-fetch a result via a plain GET without resending the query body, a `2xx` QUERY
response MAY carry (RFC 10008 [§2.3](https://www.rfc-editor.org/rfc/rfc10008#section-2.3),
[§2.4](https://www.rfc-editor.org/rfc/rfc10008#section-2.4)):

- **`Content-Location`** — a URI identifying a resource from which *this* representation can be
  retrieved by GET; and/or
- **`Location`** — a URI for the *equivalent resource* (RFC 10008
  [§2.2](https://www.rfc-editor.org/rfc/rfc10008#section-2.2)) at which the client can re-perform
  the query by GET without the body.

**Rules**
1. When present, `Content-Location`/`Location` URIs SHOULD be stable enough to GET for the life of
   the response's freshness, and a server MUST NOT embed sensitive query content in them (§4.9,
   RFC 10008 [§4](https://www.rfc-editor.org/rfc/rfc10008#section-4)).
2. The envelope's optional `$uri` member (§6.5) echoes the `Content-Location` value so the
   re-fetch reference survives when only the body is retained.
3. A server MAY instead answer indirectly with **303** See Other pointing at such a resource
   (RFC 10008 [§2.5](https://www.rfc-editor.org/rfc/rfc10008#section-2.5)).

## 4.9 — CORS preflight

The QUERY method is **not** CORS-safelisted, so a cross-origin browser client triggers a CORS
preflight (`OPTIONS`) before issuing QUERY (RFC 10008
[§4](https://www.rfc-editor.org/rfc/rfc10008#section-4)). CORS *enforcement* is a host-side
concern (D5): this repository specifies the `OPTIONS` response contract (advertising `Allow` and
`Accept-Query`, §4.6) but delegates the emission of `Access-Control-*` headers to the host adapter
(the V-07 adapter guide). Clients that cannot afford preflight MAY use the GET fallback (§4.10),
which is CORS-simple for safelisted request headers.

## 4.10 — The OPTIONAL `GET ?query=` fallback profile

Implementations MAY expose a compatibility binding: `GET /{domain}/{node}?query=` with the
URL-encoded OGraph query text in the `query` URI parameter.

```xml
<Binding Operation="GetEmployees" Method="GET" Route="/hrm/employees">
  <Map Parameter="$.query" Source="Query" Name="query" />
</Binding>
```

**Rules**
1. The GET fallback is **OPTIONAL**; conformance covers it as a separate optional profile (N-03),
   serving browsers, CORS-preflight-averse clients, and QUERY-unaware intermediaries.
2. When exposed, a GET fallback MUST return the same response envelope (§6) and the same status
   taxonomy (§4.5, mapping "missing/inconsistent body" onto a missing/malformed `query` parameter)
   as the QUERY binding for an equivalent query.
3. The fallback inherits GET caching and conditional semantics natively; it is subject to URI
   length and percent-encoding limits, which is why QUERY is primary (§4.0).

## 4.11 — Command bindings

Commands bind to the state-changing methods `POST`, `PUT`, `PATCH`, or `DELETE`, with the command
input payload carried as the request content (the parameter whose `Role` is `Input`, §3.2 rule 5).
Command bindings carry no safety guarantee (§3.1.2).

*[Owned by S-07 [O01.01.01.07]: command query-control placement — where a Command that supports
`$.query` (e.g. result projection) carries its query text. The default candidate is a reserved
member of the command payload envelope; a custom request header is the fallback only if S-07
resolves its field-value encoding and size-limit problems. Command binding conventions beyond the
method/payload mapping above are specified there.]*

# 5.0 — Query Language

The OGraph query language addresses a Root Operation and composes keyword clauses. The canonical
entry keyword is `node(...)` (alias `n(...)`); `vertex(...)`/`v(...)` are accepted synonyms
(§1.1.1).

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
   project**, regardless of their textual order in the query.

## 5.2 — Paging forms

Offset paging is carried forward from earlier drafts:

```ograph
.page(
  skip: {integer}
  take: {integer}
)
```

Cursor paging is a planned second mode.

*[Owned by S-03 [O01.01.01.03]: the committed EBNF grammar — filter operators and precedence,
functions, literals, variables, and the full paging clause syntax (offset and cursor modes).
Owned by S-04 [O01.01.01.04]: final edge-traversal syntax and traversal scoping rules.]*

# 6.0 — Communication

This section specifies the OGraph **response envelope**: the structure a server returns and a
client consumes. The transport binding — the QUERY method, the request/response media types,
`Accept-Query` discovery, the status taxonomy, caching, and `Location`/`Content-Location`
re-fetch — is §4; this section specifies the body those bindings carry. Client conformance is
defined by consuming §4 and §6 together; the client implementation itself is roadmap
L-01 [O01.01.06.01].

The normative structure of the envelope is the committed JSON Schema
[`schemas/ograph-response.schema.json`](schemas/ograph-response.schema.json) (JSON Schema draft
2020-12); the golden fixtures under [`fixtures/`](fixtures/) are its worked examples and seed the
N-03 conformance suite. Where prose and schema could diverge, the schema is authoritative for
structure and this section for meaning.

## 6.1 — The result envelope

A response body is a **result**: an object that pairs an outcome (`$status`, `$errors`) with a
payload (`$nodes`) and, for collections, paging metadata. The same result shape appears at the
root of the response and recursively at every resolved edge (§6.3). Reserved members are
`$`-prefixed; every non-`$` member of a node is projected data.

```jsonc
{
  "$status": 200,                 // HTTP-aligned status for this result scope (§6.4)
  "$uri": "/hrm/employees/~q/…",  // OPTIONAL re-fetch reference (§6.5)
  "$errors": [],                  // diagnostics for this scope (§6.4)
  "$count": 2,                    // collection only: nodes in this page (§6.6)
  "$total": 57,                   // collection only: nodes across all pages, or null (§6.6)
  "$cursor": { "next": null, "prev": null }, // collection only: cursor tokens (§6.6)
  "$nodes": [ /* node | [node,…] | null */ ] // the payload (§6.2)
}
```

**Rules**
1. Every result MUST declare `$status` (§6.4). `$errors` defaults to empty when absent.
2. A result is carried with media type `application/vnd.ograph+json` (§4.4).
3. A result MUST NOT carry members other than those defined in this section; unknown `$`-members
   are invalid (the schema sets `additionalProperties: false` on the result object).

## 6.2 — `$nodes`: single vs. collection composition

`$nodes` carries the payload and its JSON shape encodes result cardinality (§2.3.1):

- a **single-cardinality** result (`Result Cardinality="One"`, or a `One` edge) sets `$nodes` to a
  single **node object**;
- a **collection** result (`Cardinality="Many"`) sets `$nodes` to an **array** of node objects;
- a result with no representation — an error result, or a failed edge (§6.4) — sets `$nodes` to
  **`null`**.

**Rules**
1. Paging metadata (`$count`/`$total`/`$cursor`, §6.6) MAY appear only on a collection result; it
   MUST NOT appear on a single-cardinality result.
2. An empty collection is a successful result with `$nodes: []`, `$count: 0`, and (when computed)
   `$total: 0`. An empty result is **not** an error (fixture `05-empty-result`).
3. A node object's non-`$` members are exactly the members selected by `.project()` (§5.1),
   subject to member exposure (§3.3.3). A node MAY carry `$uri` (§6.5) and `$edges` (§6.3).

## 6.3 — `$edges`: traversal composition

When a query traverses `.edge(...)` (§5.1), each traversed edge appears under the node's reserved
`$edges` member, keyed by edge name. Each value is **itself a result** (§6.1) — recursively — so
an edge to a `Many` target is a collection result and an edge to a `One` target is a
single-cardinality result.

```jsonc
"$edges": {
  "addresses":      { "$status": 200, "$count": 1, "$total": 1, "$nodes": [ /* … */ ] },
  "primaryAddress": { "$status": 200, "$nodes": { /* … */ } }
}
```

**Rules**
1. `$edges` keys MUST be edge names resolvable in the containing node's scope (§3.3.3 rule 5); a
   key matches `^[A-Za-z_][A-Za-z0-9_.@-]*$`.
2. A nested edge result obeys every rule of §6.1–§6.6, including carrying its own `$status` and
   `$errors` (§6.4) and, for collection edges, its own paging metadata (§6.6).
3. `$edges` nests to arbitrary depth, mirroring the query's traversal chain (fixture
   `03-edge-traversal-nested`).

## 6.4 — `$status` / `$errors`: outcome and partial failure

Every result scope carries its **own** outcome. The root `$status` mirrors the transport response
status (§4.5); each nested edge result carries an independent `$status`, so an edge MAY fail while
its parent succeeds.

**Rules**
1. `$status` is an HTTP status code (RFC 9110 §15) describing *this* result scope.
2. A **partial failure** is a `2xx` root whose descendant edge result carries a `4xx`/`5xx`
   `$status`: the failed edge sets `$nodes: null` and populates its `$errors`, while the root and
   sibling edges remain successful. A failed edge MUST NOT change an ancestor's `$status` (fixture
   `04-partial-failure-edge`).
3. `$errors` is an array of diagnostics scoped to its result. Each diagnostic MUST carry a `code`
   and `message`; it MAY carry `severity` (`info`|`warning`|`error`, default `error`), a `target`
   (the offending member, keyword, or clause), a `position` (zero-based `line`/`column`/`offset`/
   `length` in the query text), and nested `details`.
4. Query-syntax diagnostics reuse the OGraph **G-code** vocabulary (the `G0000`–`G0008` family
   emitted by the parser); a 400 syntax error (§4.5) carries the G-code and `position` (fixture
   `06-400-syntax-error`), and a 422 policy violation carries the semantic code and `target`
   (fixture `08-422-policy-violation`). The error-code catalogue is aligned with the Core error
   model (roadmap C-01 [O01.01.04.01]).

## 6.5 — `$uri`: re-fetch reference

A result MAY carry `$uri`, a URI reference for the represented scope. On a `2xx` result it echoes
the response's `Content-Location` (§4.8) so a client that retains only the body still holds a
GET-able re-fetch reference. On an individual node, `$uri` is that node's stable identity/self
reference. `$uri` is a reference for retrieval only; it MUST NOT be required to interpret the
payload, and servers MUST NOT embed sensitive query content in it (§4.9).

## 6.6 — Paging metadata

A collection result carries paging metadata alongside `$nodes`:

- **`$count`** — REQUIRED on a collection result: the number of nodes in *this* page of `$nodes`.
- **`$total`** — OPTIONAL: the total number of nodes across all pages when the server computes it;
  **`null`** when unknown or intentionally withheld (common under cursor traversal).
- **`$cursor`** — OPTIONAL: `{ "next": …, "prev": … }`, opaque continuation tokens for cursor-mode
  paging; each is a token string or `null` at a boundary.

**Rules**
1. Offset paging (§5.2) populates `$count`/`$total` and leaves `$cursor` members `null` (fixtures
   `02-filter-sort-page`, `01-basic-project`).
2. Cursor paging populates `$cursor.next`/`$cursor.prev` and MAY report `$total: null` (fixture
   `12-cursor-page`). The cursor **placeholders are reserved now**; the query-side cursor clause
   syntax is owned by S-03 [O01.01.01.03] and does not alter this envelope shape.
3. Each collection scope pages independently: a collection edge (§6.3) carries its own
   `$count`/`$total`/`$cursor`.

## 6.7 — Relationship to the legacy response sketch

This envelope modernizes the pre-specification sketch at
`.designing/_old/responses/response.json`. Retained: the `$status`/`$errors`/`$nodes`/`$edges`
composition, per-edge status, and `$count`/`$total` paging counts. Deliberate deviations:

| Legacy sketch | This specification | Why |
| --- | --- | --- |
| Top-level `"$status": 204` on a body-bearing response | `200` for a result with a body; `204`/`304` carry **no** body (§4.7, §4.9) | RFC 9110: 204/304 responses have no content |
| In-band `"$schema"` member in the body | Removed; schema is bound by the media type + committed schema (§6.1 rule 3, §4.4) | The envelope is data, not a schema-carrying document |
| Node-level `"$label": ""` | Removed (unused) | No defined semantics |
| `"timestamp": "2024-08-13T08:32:02.-04:00"` (malformed) | RFC 3339 timestamps in fixtures | Well-formed date-time |
| `$count`/`$total` on a single-cardinality edge (`primaryAddress`) | Paging metadata is **collection-only** (§6.2 rule 1) | A `One` edge does not page |
| (absent) | `$cursor` placeholders (§6.6) | Reserve the cursor-paging shape ahead of S-03 |

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
| Legacy response sketch (`204`-with-body, in-band `$schema`/`$label`, single-edge paging counts) | modernized response envelope + committed JSON Schema | §6.7, §6.1 |

## Appendix B — Recorded deltas from the ratified layering proposal

| Proposal construct | Disposition |
| --- | --- |
| Multi-entity Node bindings (`EntityTypeRef`, `DefaultEntityType`) | **Deferred** — single Entity Type per Node retained (§2.3.2) |
| Split `gcapx`/`gbindx` document formats | **Deferred indefinitely** — single-file `gdmx` adopted (§1.3) |
| Command `Action` taxonomy (`Create\|Update\|Patch\|Delete\|Custom`) | **Deferred** — commands are named capabilities without an action attribute (§3.1.2) |
| Edge-scoped queries exposing the edge payload alongside the target payload | **Deferred** — revisit under S-04/G-03 (§3.1.3) |
| Semantic parameter roles (`Key\|Argument\|Input\|Context\|Cursor`) | **Adopted** (§3.2 rule 5) |
| Field capability facets (`Selectable`/`Filterable`/`Sortable` attributes) | **Not adopted** — expressed via Property-hosted policies instead (§2.3.6) |
