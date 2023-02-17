
# Rules
| Codes        | Description |
|--------------|-------------|
| OGQL0078     | The Select and Summarize clause cannot be used together


```

```


```

https://endpoint?$query=(
    from('users', 'u')
        .filter(startsWith(u.firstName, 'c', true))
        .select('u.firstName, u.lastName')
        .top(20)
        .skip(0)
        .resolve('addresses', 'a')
            .select('a.streetOne, a.streetTwo')
            .top(10)
            .skip(0)
        .resolve('details')
)

query=from([users], [u]).select([u].[firstName],[u].[lastName]).filter(startsWith([u].[firstName], 'c', true) and endsWith([u].[lastName], 'e', true)).skip(0).take(25)

$parameters=(
    pageNumber: 1
    pageSize: 25=
)&
$query=
    from(
        users
    )
    .select(
        firstName
        lastName
        middleName
        addresses as userAddresses {
            streetOne
            streetTwo
        }
        auditEntry {
            createdBy
            createdDateTime
            updatedBy
            updatedDateTime
        }
    )
    .filter(
        (
            firstName 
            startsWith(

            ) and 
            endsWith(

            )
        ) or (

        )
    )
    .page(
        skip @pageNumber
        take @pageSize
    )
    .resolve(
        from(

        )
    )
```