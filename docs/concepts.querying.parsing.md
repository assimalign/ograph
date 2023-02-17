

# Overview
When a query is submitted to the server it syntax is parsed in evaluated within three parts
- **Lexing**: parsing
- Parsing
- **Visiting**: Visitors are used as default 



# Expression Tree

## Tree View 
The below diagram represents the node breakdown of a parsed query
```
├── query
├── project
│   └── field 
│       ├── member
│       ├── function
│       │   └── parameter 
│       │       ├── function
│       │       ├── member
│       │       └── constant
│       └── project
│            
├── filter
├── sort
│   └── field 
│       ├── member
│       └── function
│           └── parameter 
│               ├── function
│               ├── member
│               └── constant
└── page
    ├── take
    │   └── constant (long/parameter)
    │
    ├── skip
    │   └── constant (long/parameter)
    │
    └── token
        └── constant (string/parameter)
```

## 