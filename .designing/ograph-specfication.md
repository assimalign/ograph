# 1.0 - Introduction
The Open Graph Protocol (OGraph) is an application-level protocol derived from Graph theory to implement queryable and interoperable REST services over HTTP while adhering to Domain-Driven Design concepts. This specification defines the core semantics and behavior of the OGraph protocol. OGraph seeks to solve a convention problem rather than a code problem.

## 1.1 - Terminology
The key words “MUST”, “MUST NOT”, “REQUIRED”, “SHALL”, “SHALL NOT”, “SHOULD”, “SHOULD NOT”, “RECOMMENDED”, “MAY”, and “OPTIONAL” in this document are to be interpreted as described in [**RFC2119**](https://www.rfc-editor.org/rfc/rfc2119).

- [1.0 - Introduction](#10---introduction)
  - [1.1 - Terminology](#11---terminology)
- [2.0 - GDM (Graph Data Model)](#20---gdm-graph-data-model)
  - [2.2 - `<Graph ../>`](#22---graph-)
  - [2.3 - Structural Elements](#23---structural-elements)
    - [2.3.1 - `<Node ../>`](#231---node-)
    - [2.3.2 - `<Edge ../>`](#232---edge-)
    - [2.3.3 - `<Type ../>`](#233---type-)
      - [2.3.3.1 - Scalar Type](#2331---scalar-type)
      - [2.3.3.2 - Enum Type](#2332---enum-type)
        - [2.3.3.2.1 - Member](#23321---member)
      - [2.3.3.3 - Complex Type](#2333---complex-type)
        - [2.3.3.3.1 - `<Property ../>`](#23331---property-)
          - [2.3.3.3.1.1 - `<Policy ../>`](#233311---policy-)
          - [2.3.3.3.1.2 - Reserved Property Policies](#233312---reserved-property-policies)
        - [2.3.3.3.2 - `<Function ../>`](#23332---function-)
      - [2.3.3.4 - Collection Type](#2334---collection-type)
        - [2.3.3.4 `<Item ../>`](#2334-item-)
      - [2.3.3.5 - Entity Type](#2335---entity-type)
        - [2.3.3.5.1  - `<Key ../>`](#23351----key-)
        - [2.3.3.5.2 - `<Property ../>`](#23352---property-)
        - [2.3.3.5.3 - `<Function ../>`](#23353---function-)
  - [2.4 - Behavioral Elements](#24---behavioral-elements)
    - [2.4.1 Operations](#241-operations)
      - [2.4.1.1 - `<Query ../>`](#2411---query-)
      - [2.4.1.2 -  `<Command ../>`](#2412----command-)
      - [2.4.1.3 - `<Parameter ../>`](#2413---parameter-)
        - [2.4.1.3.1 - `<Policy ../>`](#24131---policy-)
        - [2.4.1.3.2 - Reserved Parameters](#24132---reserved-parameters)
        - [2.4.1.3.3 - Reserved Query Policies](#24133---reserved-query-policies)
    - [2.4.3 - Event](#243---event)
    - [2.4.4 - Directive](#244---directive)
- [3.0 - Bindings](#30---bindings)
  - [Query Binding](#query-binding)
  - [Command Binding](#command-binding)
- [4.0 - Language](#40---language)
  - [4.1 - Keywords](#41---keywords)
    - [4.1.1 `.node()`](#411-node)
    - [4.1.2 Filtering - `.filter()`](#412-filtering---filter)
    - [4.1.3 Sorting - `.sort()`](#413-sorting---sort)
    - [4.1.4 Paging - `.page()`](#414-paging---page)
      - [4.1.3.0 Offset Paging](#4130-offset-paging)
      - [4.1.3.1 Cursor Paging](#4131-cursor-paging)
    - [4.1.5 Projections - `.project()`](#415-projections---project)
  - [4.2 Functions](#42-functions)
    - [4.2.1 String Functions](#421-string-functions)
  - [4.3 Operators](#43-operators)
    - [4.1.6 Edging - `.edge()`](#416-edging---edge)
- [5.0 - Communication](#50---communication)
  - [5.1 - Server](#51---server)
    - [5.1.1 Routing](#511-routing)
  - [5.2 - Client](#52---client)

# 2.0 - GDM (Graph Data Model)
The Graph Data Model describes the structure of the data regardless of how it is stored. The model seeks to define not only the structure of the data but the behavior of a given domain which is represented within a single Graph.

A given Graph Data Model MAY have multiple graphs defined while a single graph acts as the bounded contexts of a single domain.

---

## 2.2 - `<Graph ../>`
The Graph element is the root element that encapsulates the entire model. Though unlike a traditional data model the Graph Data Model for OGraph seeks to define not only the structure but the behavioral components.

<br/>

**Attributes**
- `Domain` - *The bounded context represented by the Graph*
- `Alias (Optional)` - *Used to avoid naming conflicts between Graph Models that may have the same name*

**Element Definition**
```xml
<Graph Domain="{Graph Domain}" Alias="{An alias name}">
<!-- The Graph Model -->
</Graph>
```
<br/>

**Rules**
1. Graph elements MAY be defined more than once within a model.
2. All Graph `Domain` attribute values MUST be unique within a given model.
3. A Graph `Alias`, when defined, MUST be unique within a given model.

---

## 2.3 - Structural Elements
Structural Elements can be broken up into two categories:

1. **Graph Identity Primitives** - Structural elements that define graph-facing identity, addressability, and traversal.
2. **Runtime Value Contracts** - Structural elements that define payload shape, type checking, validation, and serialization behavior.

---

### 2.3.1 - `<Node ../>`
A Node/Vertex represents one or more entities within a graph and acts as an **entity anchor** within the graph model. Nodes are graph-facing structural elements responsible for identity, addressability, and traversal. A node can be considered conceptually similar to a table, though unlike a record the data is stored in key-value pairs called properties through its bound `Entity Type`.

It is important to note that the terms Node and Vertex are often used interchangeably and SHOULD always be considered the same.

<br/>

>**Element Definition**
>```xml
><Node Name="{Label of Node}" Type="{Entity Type}" />
>```

<br/>

**Rules**
1. Nodes MUST ALWAYS be bound to an Entity Type.
2. No more than one Node can be bound to the same Entity Type.

---

### 2.3.2 - `<Edge ../>`
An edge represents a first-class relationship between two nodes. Unlike a traditional ER (Entity Relationship) model, relationships are not required to live on the node or entity type itself. Edges define traversal semantics within the graph while remaining independent from any specific data source implementation.

>**Element Definition**
>```xml
><Edge Name="{Name}" Source="{Source Node}" Target="{Target Node}" />
>```
**Rules**
1. The `Source` attribute MUST reference a defined Node in the current Graph.
2. The `Target` attribute MUST reference a defined Node in the current Graph, or a fully qualified Node reference in another Graph.
3. More than one Edge MAY exist between the same two Nodes.
4. An Edge signature, defined by the tuple (`Name`, `Source`, `Target`), MUST be unique within a Graph.

---

### 2.3.3 - `<Type ../>`
A Type defines the **runtime contract** for values and payloads within a graph model. Types are payload-facing structural elements responsible for describing shape, type checking, validation, coercion, and serialization behavior. While Nodes and Edges define graph semantics, Types define how values are represented and processed at runtime.

- [Scalar Type](#scalar-type)
- [Enum Type](#enum-type)
- [Complex Type](#complex-type)
- [Collection Type](#collection-type)
- [Entity Type](#entity-type)

>**Element Definition**
>```xml
><Type Name="{Type Name}" Kind="{Scalar|Enum|Complex|Collection|Entity}" />
>```
**Rules**
1. Types MUST control serialization and type checking.
2. All Type Names MUST be unique.

---

#### 2.3.3.1 - Scalar Type
A `Scalar Type` represents a single, indivisible value, which are derived from Primitive Types that must be supported by OGraph. The following Scalar be derived from the following primitive types:
- Integer
- Float
- String
- Boolean

**Rules**

---

#### 2.3.3.2 - Enum Type
##### 2.3.3.2.1 - Member

---

#### 2.3.3.3 - Complex Type
A `Complex Type` represents a structured runtime contract composed of properties and functions. Complex types are reusable value objects and are commonly used for nested payloads, query policy contracts, and other non-entity structures.

>**Element Definition**
>```xml
><Type Name="{Type Name}" Kind="{Scalar|Enum|Complex|Collection|Entity}" />
>```
##### 2.3.3.3.1 - `<Property ../>`
The `Property` element defines a named member of a `Complex Type` or `Entity Type`.

```xml
<Property Name="{Property Name}" Type="{Type Name}" IsReadOnly="{true|false}" IsNullable="{true|false}">
  <Policy Name="{Policy Name}">
    <Operator Name="{Operator Name}" />
    <Function Name="{Function Name}" />
    <Option Name="{Option Name}" Value="{Option Value}" />
  </Policy>
</Property>
```

**Rules**
1. Property names MUST be unique within the declaring Type.
2. A Property `Type` MUST reference a defined Type.
3. A Property MAY declare zero or more `Policy` child elements.
4. A Property MAY be read-only or nullable as defined by its attributes.

###### 2.3.3.3.1.1 - `<Policy ../>`
When declared beneath a `Property`, a `Policy` defines the member-level capability of that property within OGraph query semantics. Property policies describe the maximum set of operators, functions, and options that a property supports regardless of which operation exposes it.

```xml
<Policy Name="{Policy Name}">
  <Operator Name="{Operator Name}" />
  <Function Name="{Function Name}" />
  <Option Name="{Option Name}" Value="{Option Value}" />
</Policy>
```

**Rules**
1. Policy names MUST be unique within the containing Property.
2. A Property-hosted Policy MUST NOT declare a `Type` attribute.
3. A Property-hosted Policy MAY declare zero or more `Operator`, `Function`, or `Option` child elements.
4. Property policies define the maximum capability of the containing Property.
5. If a Property does not declare a reserved policy, the Property falls back to default type semantics and implementation defaults.
6. Property policies MUST remain data-source agnostic and describe only portable query behavior.

###### 2.3.3.3.1.2 - Reserved Property Policies
The following Policy names are reserved by OGraph when declared beneath a `Property`:

1. `$.filter`
2. `$.sort`
3. `$.project`

**Rules**
1. A `$.filter` policy defines the operators and functions that MAY be used when the Property is referenced in `.filter()`.
2. A `$.sort` policy defines whether the Property MAY participate in `.sort()` and any supported sorting options.
3. A `$.project` policy defines whether the Property MAY participate in `.project()` and any supported projection options.
4. A Property policy MAY narrow the behavior otherwise implied by its Type.
5. Operation-level policies declared beneath `$.query` MAY expose only a subset of the capability defined by Property policies.
6. The effective behavior of a Property in a given operation scope is the intersection of the Property policy and the matching operation-level policy.
7. If an operation-level policy broadens a Property policy, the model is invalid.

##### 2.3.3.3.2 - `<Function ../>`
#### 2.3.3.4 - Collection Type
##### 2.3.3.4 `<Item ../>`
#### 2.3.3.5 - Entity Type

>**Element Definition**
>```xml
><Type Name="{Type Name}" Kind="Entity">
>  <!-- Key, Property, or Function definitions -->
></Type>
>```
**Rules**


##### 2.3.3.5.1  - `<Key ../>`

>**Element Definition**
>```xml
><Key Property="{Property Name}" />
>```
**Rules**
##### 2.3.3.5.2 - `<Property ../>`
The `Property` element defined in [2.3.3.3.1 - `<Property ../>`](#23331---property-) is also used for `Entity Type` members. All Property policy rules and reserved Property policies apply equally to Entity Types.
##### 2.3.3.5.3 - `<Function ../>`

## 2.4 - Behavioral Elements
Behavioral elements define executable capabilities over a Graph Model. Queries and Commands are bound to graph scopes and may expose additional query-control behavior through reserved parameters.

### 2.4.1 Operations
Operations can be broken into two scopes:

1. **Root Operations** - Operations bound to a `Node`. These act as the entry points into a Graph.
2. **Resolver Operations** - Operations bound to an `Edge`. These act as resolvers when `.edge(...)` is invoked within an OGraph query.

For operation resolution, an operation call signature is defined by the operation kind, the bound scope (`Node` or `Edge`), and the set of non-reserved parameter names exposed by the operation.

#### 2.4.1.1 - `<Query ../>`
Queries define data retrieval.
```xml
<Query Name="{Query Name}" Node="{Node Name}" Edge="{Edge Name}">
<!-- Parameters -->
</Query>
```

**Rules**
1. A Query MUST declare exactly one of `Node` or `Edge`.
2. A Query `Node` attribute, when present, MUST reference a defined Node in the current Graph.
3. A Query `Edge` attribute, when present, MUST identify a single defined Edge in the current Graph.
4. A Node-bound Query acts as a Root Operation.
5. An Edge-bound Query acts as a Resolver Operation.
6. Query names MUST be unique within a Graph.
7. No two Queries bound to the same `Node` or `Edge` MAY expose the same call signature.

---


#### 2.4.1.2 -  `<Command ../>`
Commands Represent state change.

```xml
<Command Name="{Command Name}" Node="{Node Name}" Edge="{Edge Name}">
<!-- Parameters -->
</Command>
```

**Rules**
1. A Command MUST declare exactly one of `Node` or `Edge`.
2. A Command `Node` attribute, when present, MUST reference a defined Node in the current Graph.
3. A Command `Edge` attribute, when present, MUST identify a single defined Edge in the current Graph.
4. A Node-bound Command acts as a Root Operation.
5. An Edge-bound Command acts as a Resolver Operation.
6. Command names MUST be unique within a Graph.
7. No two Commands bound to the same `Node` or `Edge` MAY expose the same call signature.

---

#### 2.4.1.3 - `<Parameter ../>`
Parameters define arguments accepted by a Query or Command. Parameters may either represent ordinary operation arguments or reserved protocol arguments.

```xml
<Parameter Name="{Parameter Name}" Type="{Type Name}" IsRequired="{true|false}">
  <Policy Name="{Policy Name}" Type="{Type Name}" />
</Parameter>
```

**Rules**
1. Parameter names MUST be unique within the containing operation.
2. Reserved parameter names MUST begin with `$.`.
3. Non-reserved parameters participate in operation call signature resolution.
4. A Parameter MAY declare zero or more `Policy` child elements.

##### 2.4.1.3.1 - `<Policy ../>`
When declared beneath a `Parameter`, a Policy defines query-control behavior attached to that Parameter. Policies keep the Parameter element generic while allowing the model or implementations to add new behaviors without changing the core Parameter shape.

```xml
<Policy Name="{Policy Name}" Type="{Type Name}" />
```

**Rules**
1. Policy names MUST be unique within the containing Parameter.
2. A Policy `Type` MUST reference a defined `Complex Type`.
3. Policy types MUST describe query-visible members rather than storage-specific or data-source-specific paths.
4. Policy types MAY include nested complex members to describe relationship-scoped query members.
5. Policy resolution MUST be relative to the `Node` or `Edge` bound by the containing operation.
6. Policy types MUST NOT require explicit edge-path annotations in order to expose relationship-scoped members.

##### 2.4.1.3.2 - Reserved Parameters
The following parameters are reserved by OGraph:

1. `$.query`

The `$.query` parameter is reserved for OGraph query text and query-control policies.

**Rules**
1. The `$.query` parameter SHOULD be declared using a string-compatible scalar type.
2. The `$.query` parameter MAY declare one or more `Policy` child elements.
3. A Query or Command that supports OGraph query keywords such as `.filter()`, `.sort()`, `.project()`, or `.page()` MUST expose them through policies declared on `$.query`.

##### 2.4.1.3.3 - Reserved Query Policies
The following Policy names are reserved by OGraph when declared beneath `$.query`:

1. `$.filter`
2. `$.sort`
3. `$.project`
4. `$.page`

**Rules**
1. A `$.filter` policy defines the members available to `.filter()`.
2. A `$.sort` policy defines the members available to `.sort()`.
3. A `$.project` policy defines the members available to `.project()`.
4. A `$.page` policy defines the members available to `.page()`.
5. If a reserved policy is not declared for an operation, the corresponding query keyword MUST be considered unavailable in that operation scope.
6. Implementations MAY define additional custom policies beneath `$.query`.
7. Custom policy names SHOULD use a namespace or vendor prefix to avoid collisions with reserved policy names.



### 2.4.3 - Event
### 2.4.4 - Directive

---

# 3.0 - Bindings
Bindings that map HTTP Endpoints to specific Queries, Commands, or Events must follow a specific convention.

1. Literal Segments of routes for Root Operations MUST match the target Node within a given Graph.
2. All routes MUST have a root literal segment that matches the `Domain` or `Alias` of a given Graph.
3. Resolver Operations bound to an Edge MAY be executed as part of OGraph query resolution and do not require independent public HTTP routes unless explicitly bound.

## Query Binding
## Command Binding

# 4.0 - Language
This section defines the language specification for OGraph query. When a given query is executed the following order of operations is carried out in the defined order.

## 4.1 - Keywords

### 4.1.1 `.node()`
.node() selects a Node-bound Root Operation as the starting scope of an OGraph query.
### 4.1.2 Filtering - `.filter()`
.filter() MAY only be used when the current operation exposes a `$.query` parameter with a `$.filter` policy.
### 4.1.3 Sorting - `.sort()`
.sort() MAY only be used when the current operation exposes a `$.query` parameter with a `$.sort` policy.
### 4.1.4 Paging - `.page()`
.page() MAY only be used when the current operation exposes a `$.query` parameter with a `$.page` policy. There are two options.

#### 4.1.3.0 Offset Paging

```ograph
.page(
  skip: # integer
  take: # Integer
)
```

#### 4.1.3.1 Cursor Paging

---

### 4.1.5 Projections - `.project()`
.project() MAY only be used when the current operation exposes a `$.query` parameter with a `$.project` policy.

## 4.2 Functions

### 4.2.1 String Functions 

## 4.3 Operators
---

### 4.1.6 Edging - `.edge()`
.edge() resolves against Edge-bound Queries or Commands available within the current scope. If more than one Resolver Operation in the same scope exposes the same call signature, the model is invalid.

# 5.0 - Communication

## 5.1 - Server

### 5.1.1 Routing

**Rules**
1. All literal segments in a given HTTP route MUST match the name of a defined node within a Graph Model.
2. The beginning literal segment of a route MUST start with the Graph Domain or Alias.

## 5.2 - Client
