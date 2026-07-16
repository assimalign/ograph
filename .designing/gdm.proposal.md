# OGraph GDM Proposal

## Why this proposal exists

The repository currently has three competing concerns that are partially mixed together:

1. The graph model itself: graphs, nodes, edges, and types.
2. The query and execution model: what can be selected, filtered, traversed, and mutated.
3. The transport model: HTTP routes, methods, media types, headers, and parameter locations.

That makes the design hard to stabilize because the GDM ends up carrying transport details that are not required to understand the graph, while the syntax and server still need graph-aware metadata that is richer than plain DTO types.

This proposal keeps the graph model transport-neutral, but still makes it useful for:

- query validation
- projection analysis
- edge traversal planning
- filter and sort capability checks
- resolver planning
- protocol binding

## Design goals

The proposed GDM should:

1. Model the service as a graph first, not as HTTP routes first.
2. Keep nodes clean and make relationships first-class.
3. Support schema-driven query analysis.
4. Allow multiple protocol bindings over the same graph model.
5. Avoid opaque metadata for concepts that affect execution or validation.
6. Keep HTTP/OpenAPI concerns outside the core model.

## Recommended layering

Use three layers:

### 1. Core GDM

This is the protocol-neutral graph schema.

It describes:

- `Model`
- `Graph`
- `Type`
- `Node`
- `Edge`
- `Directive`
- `Annotation`

This layer is enough to answer:

- What nodes exist?
- What does each node return?
- What fields are selectable?
- What edges can be traversed?
- What is the target of a traversal?
- Is the traversal one-to-one or one-to-many?
- What functions exist on fields or types?

### 2. Capability Model

This is still transport-neutral, but it describes executable intent.

It describes:

- root queries
- edge traversals
- commands
- events
- subscriptions

This layer is enough to answer:

- Which nodes are entry points?
- Which commands operate on which node or edge?
- Which parameters are semantic keys vs arbitrary arguments vs input payloads?
- What shape comes back from a query or command?

### 3. Protocol Bindings

This is where HTTP belongs.

It describes:

- method
- route template
- path/query/header/body parameter mapping
- media types
- auth hints
- OpenAPI details

This layer is enough to answer:

- How does an HTTP request reach a capability?
- How are route parameters mapped to semantic parameters?
- Which media types are supported?

## Core GDM

### Model

The root document should primarily contain graphs and shared definitions.

Suggested top-level shape:

- `Model`
- `Types`
- `Graphs`
- `Capabilities`
- `Bindings`

If you want a single-file design, keep all five sections in one document. If you want a cleaner long-term format, split them into:

- `gdmx`: core model
- `gcapx`: capability model
- `gbindx`: protocol bindings

## Graph

A graph is a bounded domain or subgraph.

Suggested responsibilities:

- namespace/domain boundary
- owned nodes
- owned edges
- graph-local directives or annotations

Suggested attributes:

- `Name`
- `Namespace` or `Domain`
- optional `Alias`

## Types

Keep the current broad categories, but simplify how collections are represented.

Recommended type kinds:

- `Scalar`
- `Enum`
- `Complex`
- `Entity`

Types should remain structural descriptors only.

That means:

- a type describes shape
- a type does not define graph identity
- a type does not define graph relationships
- a type can be reused by nodes, edges, parameters, and results

This is an important boundary because the graph should not treat relationship identity as a kind of type. The relationship itself is the edge.

At the same time, types are not passive documentation objects.

From a runtime perspective, a type can and should control implementation concerns such as:

- request deserialization
- response serialization
- value coercion
- type checking
- validation
- equality or comparison rules where needed
- container behavior for collection types

So a better way to phrase the boundary is:

- `Type` owns value and payload semantics
- `Node` and `Edge` own graph semantics

That lets types remain implementation-significant without making them the owner of graph identity or relationship identity.

### Collection types should stay, but with a narrower job

Collection or container types are still useful in the structural model.

They should exist for implementation-significant shapes such as:

- arrays
- lists
- sets
- maps or dictionaries
- keyed collections

What they should not be used for is graph traversal multiplicity by themselves.

That is a different concern.

So the distinction should be:

- `CollectionType` answers: what is the structural container shape?
- `Cardinality` answers: does this node, edge, or capability yield one or many graph results?

Examples:

```xml
<CollectionType Name="EmployeeList" Kind="List">
  <Item Type="Employee" />
</CollectionType>
```

```xml
<CollectionType Name="EmployeeByIdMap" Kind="Dictionary">
  <Key Type="EmployeeId" />
  <Value Type="Employee" />
</CollectionType>
```

```xml
<Result Type="Employee" Cardinality="Many" />
```

Those are not the same thing:

- a traversal can return many `Employee` results without introducing a named collection type
- a capability can also explicitly return a dictionary or another container when that container shape matters to the contract or implementation

So the rule should be:

- keep collection types in the type system
- do not overload collection types to mean graph multiplicity

## Type references

Use a first-class type reference shape everywhere a type is consumed.

Suggested fields:

- `Type`
- `Cardinality`: `One | Many`
- `Nullable`

This keeps property, parameter, result, and edge definitions consistent.

`Cardinality` should describe graph semantics, not container internals.

So if a result type is a dictionary, list, or map, the type reference still points to that container type, while `Cardinality` describes whether the capability yields one result object or many result objects.

In other words:

- the type reference tells the runtime how to interpret the value
- the cardinality tells the graph engine how to interpret the result set

## Node

Canonicalize on `Node` in the schema and runtime. Keep `Vertex` as a query-language alias if you want it.

A node should represent a queryable graph anchor, not a type definition.

As a rule, nodes should only be bound to entity types.

Suggested attributes:

- `Name`
- `EntityType` or `EntityTypes`
- optional `DefaultEntityType`
- optional `KeySet`

Suggested responsibilities:

- points to one or more entity types
- defines node-level capabilities and directives
- acts as the source or target for edges

Nodes should not own HTTP routes.

### Why a node can reference multiple entity types

If a node can represent more than one type, the node should hold entity bindings rather than collapsing into a single concrete entity type.

That gives you room for:

- polymorphic resources
- variant projections
- domain overlays
- alternate structural views over the same graph anchor

So the rule becomes:

- `Node` is the graph subject
- `EntityType` is the structural identity of node payloads
- other types remain supporting structural types

not:

- node equals entity type

But the allowed node-bound type family should still be constrained to entity types only.

That means:

- nodes bind to `EntityType`
- properties on entities may use `ComplexType`, `ScalarType`, `EnumType`, and `CollectionType`
- edges may use those same supporting types for relationship fields
- nodes do not bind directly to scalar, complex, enum, or collection types

## Edge

An edge should be a first-class graph object with explicit traversal semantics.

The edge is the relationship.

Suggested attributes:

- `Name`
- `Source`
- `Target`
- `Cardinality`
- optional `Inverse`
- optional `IsDerived`
- optional `SourceTypes`
- optional `TargetTypes`

Suggested responsibilities:

- connects two nodes
- describes multiplicity
- owns the relationship semantics
- can carry its own fields
- can be analyzed independently of transport

### Edge examples

Simple edge:

```xml
<Edge Name="Addresses"
      Source="Employees"
      Target="Addresses"
      Cardinality="Many" />
```

Relationship-bearing edge:

```xml
<Edge Name="AssignedTo"
      Source="Users"
      Target="Roles"
      Cardinality="Many">
  <Property Name="AssignedAt" Type="DateTime" />
  <Property Name="AssignedBy" Type="UserId" />
  <Property Name="Scope" Type="RoleScope" />
</Edge>
```

Derived edge over a multi-hop traversal:

```xml
<Edge Name="AddressTypes"
      Source="Employees"
      Target="AddressTypes"
      Cardinality="Many"
      IsDerived="true" />
```

Derived edges are a better fit than named `Path` elements when what you really want is a stable semantic traversal.

### Why edge-owned fields matter

If relationships are first-class, fields like these belong on the edge, not on either node:

- assignment timestamp
- ranking
- state
- effective date
- strength
- role within the relationship

The edge can still use types for structure:

- property types
- reusable complex field groups
- scalar validation

but the edge itself remains the relationship object.

### Multiple edges between the same nodes

The edge identity should not be derived from `Source + Target`.

You should allow:

- multiple named edges between the same two nodes
- self-referencing edges
- differently constrained edges between the same nodes

For example, all of these should be legal and distinct:

```xml
<Edge Name="PrimaryAddress" Source="Employees" Target="Addresses" Cardinality="One" />
<Edge Name="SecondaryAddresses" Source="Employees" Target="Addresses" Cardinality="Many" />
<Edge Name="PreviousAddresses" Source="Employees" Target="Addresses" Cardinality="Many" />
```

### Node/type constraints on edges

If a node can represent multiple types, an edge may need to restrict which source and target types it applies to.

That is why `SourceTypes` and `TargetTypes` are useful.

Example:

```xml
<Edge Name="ReportsTo"
      Source="Users"
      Target="Users"
      SourceTypes="Employee"
      TargetTypes="Manager"
      Cardinality="One" />
```

## Members and field capabilities

Properties and functions should stay on types, but they need capability metadata for query analysis.

Because types also drive implementation, this is the natural place for value-level behavior such as:

- serialization format
- parser or coercion rules
- validation constraints
- function signatures
- item key or value semantics for container types

Recommended field-level facets:

- `Selectable`
- `Filterable`
- `Sortable`
- `Expandable`
- `Readable`
- `Writable`
- `Nullable`

For filters, avoid generating separate filter input types in the core model. Instead, store allowed operators or capabilities on the field itself.

Example:

```xml
<Property Name="FirstName" Type="String">
  <Capabilities Selectable="true" Filterable="true" Sortable="true" />
</Property>
```

This keeps the GDM authoritative for query validation.

For collection types, capabilities may also need container-specific metadata such as:

- key comparability
- duplicate rules
- ordering guarantees
- whether values are filterable or projectable by item type

## Types as runtime contracts

If it helps, you can think of types as serving two roles at once:

### Schema role

Types define:

- field structure
- nested shape
- scalar constraints
- enum members
- container shape

### Runtime role

Types implement:

- how values are read
- how values are written
- how values are validated
- how request and response payloads are materialized
- how collection items, keys, and values are interpreted

This maps well to the current codebase, where `GdmType` already exposes read and write behavior and is more than just a passive schema object.

## Capability model

The capability model is where execution-facing semantics should live.

Recommended capability kinds:

- `Query`
- `Command`
- `Event`
- `Subscription`

Each capability should be attached to a graph subject:

- `Node`
- `Edge`
- `Graph`

### Query capability

A query capability should express:

- what subject it starts from
- whether it is a root or traversal capability
- what parameters it accepts
- what it returns

Suggested shape:

```xml
<Query Name="GetEmployees" Scope="Root" Subject="Employees">
  <Result Node="Employees" Cardinality="Many" />
</Query>
```

Item lookup:

```xml
<Query Name="GetEmployeeById" Scope="Root" Subject="Employees">
  <Parameter Name="EmployeeId" Type="EmployeeId" Role="Key" />
  <Result Node="Employees" Cardinality="One" />
</Query>
```

Edge traversal:

```xml
<Query Name="GetEmployeeAddresses" Scope="Edge" Subject="Employees.Addresses">
  <Parameter Name="EmployeeId" Type="EmployeeId" Role="Key" />
  <Result Node="Addresses" Edge="Addresses" Cardinality="Many" />
</Query>
```

An edge-scoped query should be able to expose both:

- the target node payload
- the edge payload

because the edge is a first-class object, not just a traversal label.

### Why result should usually point to a node

For graph-facing capabilities, the result should usually point to a graph subject rather than a raw type.

That gives you:

- a stable graph identity
- a stable traversal target
- a canonical source for projection rules
- a canonical source for edge expansion rules

So the default interpretation should be:

- `Result Node="Employees"` means the payload shape comes from the entity type bound to `Employees`
- `Cardinality` describes whether one or many node instances are returned

### When result should point to a type instead

There will still be capabilities that return values that are not graph nodes, such as:

- aggregate summaries
- command receipts
- validation payloads
- dictionaries
- ad hoc structured results

For those cases, use a value-oriented result form.

Example:

```xml
<Command Name="ValidateImport" Action="Custom" Subject="Employees">
  <Parameter Name="Input" Type="EmployeeImportBatch" Role="Input" />
  <ValueResult Type="ValidationSummary" />
</Command>
```

That keeps graph results and value results distinct instead of forcing every result through raw types or forcing every result through nodes.

### Command capability

A command should be modeled semantically, not as an HTTP verb.

Suggested fields:

- `Name`
- `Action`: `Create | Update | Patch | Delete | Custom`
- `Subject`
- `Parameter`
- `Result`

Example:

```xml
<Command Name="CreateEmployee" Action="Create" Subject="Employees">
  <Parameter Name="Input" Type="EmployeeCreate" Role="Input" />
  <Result Node="Employees" Cardinality="One" />
</Command>
```

### Parameter roles

Parameter roles should be semantic, not transport-specific.

Recommended roles:

- `Key`
- `Argument`
- `Input`
- `Context`
- `Cursor`

Avoid roles like `Path`, `Query`, `Header`, and `Body` in the capability model. Those belong in protocol bindings.

## Protocol bindings

Bindings should be separate from core GDM and capabilities.

That is where you model HTTP-specific details:

- `Method`
- `Route`
- `Consumes`
- `Produces`
- parameter source mapping

Suggested shape:

```xml
<HttpBinding Capability="GetEmployees"
             Method="GET"
             Route="/employees" />

<HttpBinding Capability="GetEmployeeById"
             Method="GET"
             Route="/employees/{employeeId}">
  <ParameterMap CapabilityParameter="EmployeeId" Source="Path" Name="employeeId" />
</HttpBinding>

<HttpBinding Capability="CreateEmployee"
             Method="POST"
             Route="/employees">
  <ParameterMap CapabilityParameter="Input" Source="Body" />
</HttpBinding>
```

This is the main separation that keeps the GDM generic.

## What should not be first-class in the core model

The following should not live in the core GDM:

- HTTP method
- HTTP route
- header requirements
- query-string parameter names
- media types
- OpenAPI operation ids

If they affect wire behavior, they belong in bindings.

## What should not be hidden inside `Meta`

Anything that is required for:

- validation
- query planning
- traversal planning
- execution routing
- tooling generation

should not be encoded as opaque `Meta`.

Use `Meta` only for:

- human annotations
- vendor-specific hints
- non-semantic UI/help text

For example:

- `Method`
- `Route`
- `Cardinality`
- `Role`

should all be modeled explicitly.

## How query analysis should use the model

The syntax layer should stay syntax-only. It should produce an AST.

Then add a semantic analysis layer that resolves the AST against the GDM:

1. Resolve root node.
2. Resolve selected properties against the node type.
3. Resolve member functions against the declaring type.
4. Resolve each edge traversal against the current node.
5. Validate paging only on `Many` results.
6. Validate sorting only on sortable fields.
7. Validate filtering only on filterable fields/operators.
8. Carry the resolved node/edge/type info into an execution plan.

This means the parser should not need HTTP knowledge, and the GDM should not need route knowledge to validate a query.

## Recommended minimal schema

If you want the smallest useful design that still supports your goals, this is the minimum set I would stabilize first:

- `Model`
- `Graph`
- `Type`
- `Node`
- `Edge`
- `Query`
- `Command`
- `HttpBinding`

And within those:

- typed references with `Type`, `Cardinality`, and `Nullable`
- edge multiplicity
- edge-owned relationship fields
- explicit collection or container types
- parameter semantic roles
- field capability flags

## Example sketch

```xml
<Model Version="1.0">
  <Graph Name="Hrm">
    <Types>
      <EntityType Name="Employee">
        <Key Property="Id" />
        <Property Name="Id" Type="EmployeeId" />
        <Property Name="Info" Type="EmployeeInfo" />
      </EntityType>

      <EntityType Name="EmployeeAddress">
        <Key Property="Id" />
        <Property Name="Id" Type="AddressId" />
        <Property Name="Address" Type="Address" />
      </EntityType>

      <ComplexType Name="EmployeeInfo">
        <Property Name="FirstName" Type="String">
          <Capabilities Selectable="true" Filterable="true" Sortable="true" />
        </Property>
        <Property Name="LastName" Type="String">
          <Capabilities Selectable="true" Filterable="true" Sortable="true" />
        </Property>
      </ComplexType>

      <CollectionType Name="EmployeeByIdMap" Kind="Dictionary">
        <Key Type="EmployeeId" />
        <Value Type="Employee" />
      </CollectionType>
    </Types>

    <Nodes>
      <Node Name="Employees" DefaultEntityType="Employee">
        <EntityTypeRef Type="Employee" />
      </Node>
      <Node Name="Addresses" DefaultEntityType="EmployeeAddress">
        <EntityTypeRef Type="EmployeeAddress" />
      </Node>
    </Nodes>

    <Edges>
      <Edge Name="Addresses"
            Source="Employees"
            Target="Addresses"
            Cardinality="Many" />

      <Edge Name="PrimaryAddress"
            Source="Employees"
            Target="Addresses"
            Cardinality="One">
        <Property Name="IsPrimary" Type="Boolean" />
        <Property Name="EffectiveFrom" Type="Date" Nullable="true" />
      </Edge>
    </Edges>

    <Capabilities>
      <Query Name="GetEmployees" Scope="Root" Subject="Employees">
        <Result Node="Employees" Cardinality="Many" />
      </Query>

      <Query Name="GetEmployeeById" Scope="Root" Subject="Employees">
        <Parameter Name="EmployeeId" Type="EmployeeId" Role="Key" />
        <Result Node="Employees" Cardinality="One" />
      </Query>

      <Command Name="CreateEmployee" Action="Create" Subject="Employees">
        <Parameter Name="Input" Type="EmployeeCreate" Role="Input" />
        <Result Node="Employees" Cardinality="One" />
      </Command>
    </Capabilities>
  </Graph>

  <Bindings>
    <HttpBinding Capability="GetEmployees" Method="GET" Route="/employees" />
    <HttpBinding Capability="GetEmployeeById" Method="GET" Route="/employees/{employeeId}" />
    <HttpBinding Capability="CreateEmployee" Method="POST" Route="/employees" />
  </Bindings>
</Model>
```

## Migration path from the current repo

### Step 1

Stabilize the core terms:

- `Graph`
- `Node`
- `Edge`
- `Type`
- `Query`
- `Command`

Prefer `Node` in runtime and schema. Treat `Vertex` as a query-language synonym only.

### Step 2

Move HTTP method/route concerns out of `Meta` and out of node-level definitions into explicit protocol bindings.

### Step 3

Make `Edge` carry cardinality and its own relationship fields.

That gives the syntax and analyzer enough information to validate traversal chains while keeping the relationship on the edge itself.

### Step 4

Retain explicit collection types in the type system for implementation-relevant containers such as dictionaries, while keeping graph multiplicity on type references, edges, and capability results.

### Step 5

Move filter/sort/page semantics out of synthetic input types and into field and edge capabilities.

### Step 6

Add a semantic analysis pass in `Syntax` that resolves AST nodes against the GDM.

This semantic phase should resolve results against:

- nodes for graph results
- value types for non-graph results

### Step 7

Make `Server` resolve protocol bindings to capabilities, and capabilities to nodes/edges, instead of binding HTTP directly to nodes.

## Practical repository mapping

If you keep the current project split, the clean responsibility boundary looks like this:

- `libraries/Gdm`
  - core model
  - capability model
  - serialization
  - schema validation

- `libraries/Syntax`
  - AST
  - parser
  - semantic analyzer over GDM

- `libraries/Server`
  - HTTP bindings
  - route matching
  - capability execution
  - media-type negotiation

That keeps the GDM generic, while still making it strong enough to drive query analysis and execution planning.
