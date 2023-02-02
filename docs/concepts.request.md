



```http
QUERY /api/users?query=variables({take: 20 skip: 40}).project({ userId firstName lastName middleName 2.5 as ConstantValue }).filter({startsWith(firstName, 'c', true) and(endsWith(lastName, 'e', true) or endsWith(middleName, 'e', true)) and age eq 12}).page({take @take skip @skip}).sort({toLower(firstName) desc})


```