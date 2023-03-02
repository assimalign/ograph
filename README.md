# OGraph (Open Graph Protocol) 


Inspired by [OData (github.com)](https://github.com/OData), Graph Data Models, and [ChilliCream's (github.com)](https://github.com/ChilliCream) GraphQL framework, OGraph is an open source protocol built over HTTP intended to provide the ability to query data similarly to GraphQL while maintaining REST. 

With the world moving towards ***micro-everything***, the push for Graph based solutions has become very prevalent in today solutions.

## Key Features

## Comparisons
::
### OData vs OGraph

**Data Modeling** 
A key benefit OData has over GraphQL is the ability to extract the EDM (Entity Data Model) over the web. OGraph seeks to provide a similar approach to an EDM (Entity Data Model) via a Graph Data Model. Rather than create hard relationships between



### GraphQL vs OGraph




combing the flexibility  built for the service/application layer meant for virtualized graph services that allow for interopreble and queryable REST APIS.

**Purpose:** OGraph serves a virtualization of your app-layer architecture utilizing graph modeling concepts while offering secure accessible querying operations out of the box. 

By creating a cohesive service/application across in-cohesive data-layer, it allows for easy comprehensions of a complex system.

Most companies as they grow tend to integrate various data stores that either don't fully talk each or have a vary difficult 

 such as SQL Server, MongoDB, Neo4J, and t
When it comes to building any type of HTTP services many of the following concerns have to be addressed in all application development life-cycles:
- Request/Response: having an understanding of what to expect and what to send.
- Query Accessibility: being able to easily support and standardized queryable data that is read-only and flexible enough to implement field level security. 


Utilizes concepts of the following technologies:
- GraphQL
- OData
- Stardog

 


## Limitations
- Service-to-service query
- **Degree of Separation**: Unlike a 


# What does OGraph hope to achieve ?

1. To allow interoperable and queryable REST APIs
2. To visualize your REST services in a Graph Model
3. To structure data in terms of a Graph Data Model which can be accessible over the web
4. To maintain REST yet allow flexible query operations and data resolution like GraphQL




## Terminology: 

- Node/Vertex: 
- Arc: represents a collection of nodes
  - Head:
  - Tail:
- Label: 



$select(
    firstName,
    lastName,
    middleName
)&
$where(

)



# OGraph Model


## Resolvers

There are three types of resolvers 

- Operation Resolver
- Property Resolver(s)
- Edge Resolver(s)
