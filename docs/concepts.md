
#

# Interoperable Data Resolvers

One of the most useful benefits of GraphQL is the ability to resolve data from different data layers



```
├── Select
│   ├── Member 
│   │   ├── Member 
│   │   ├── Function (Scalar Only)
│   │   │   
│   ├── Function (Scalar Only) 
        └── Parameter
        │   │   │       ├── Member
        │   │   │       ├── Member
        │               └── Function
│   └── Value (Constant)
└── filter


```


```HTTP
QUERY /api/resource HTTP/1.1
authorization: Bearer jasfd9ouasflaseafj9asjfd
content-type: application/ograph

// Query Content
query({
    variable {
        take: 20
        skip: 40
    }
})
.project({
    userId
    firstName
    lastName
    middleName
    2.5 as ConstantValue # Constant Expression as an identifier
    (firstName eq 'chase') as isFirstNameCorrect, # Binary Expression as an identifier
    concat(firstName, ' ', middleName, ' ', lastName) as fullName
    addresses as locations {
        streetOne
        streetTwo
        streetThree
        addressTypes {
            typeId
            type
        }
    }
})
.navigate({

})
.navigate({
    
})
.filter({
    startsWith(firstName, 'c', true) and (
        endsWith(lastName, 'e', true) or 
        endsWith(middleName, 'e', true)
    ) and age eq 12
})
.page({
    take @take
    skip @skip
})
.sort({
    toLower(firstName) desc
})

```