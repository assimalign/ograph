using Assimalign.OGraph;
using Assimalign.OGraph.AspNetCore;
using System.Threading;


var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddSingleton<IRepository<User>, UserRepository>();

builder.Services
    .AddOGraphOptions(options =>
    {
        options.RoutePrefix = "api";
    })
    .AddOGraph("Users", builder =>
    {
        // Composite Implementation
        builder.AddVertex<User>(vertex =>
        {
            // Define Type information
            vertex.HasLabel("users");
            vertex.HasKey(p => p.UserId); // Composite keys NOT allowed currently

            vertex.HasProperty(p => p.Details)
                .UseType(details => // Extending the details type
                {
                    details.HasProperty("fullName")
                        .UseMiddleware(async (context, next) =>
                        {
                            if (!context!.GetClaimsPrincipal()!.Identity!.IsAuthenticated )
                            {
                                return OGraphResult.Unauthorized("The user is not authorized to access this resource.");
                            }

                            return await next(context);
                        })
                        .UseResolver(context => // Creating a resolver for a computed property
                        {
                            var parent = context.GetParent<UserDetails>();

                            return $"{parent.LastName}, {parent.FirstName} {parent.MiddleName}";
                        });
                });

            // Define Edges
            vertex.HasEdge("addresses")
                .UseMetadata("routeRepresentation", "/users/{userId}/addresses") //There should be a corresponding operation bound to the addresses vertex
                .UseTarget<UserAddressVertex>();

            vertex.HasEdge("primaryAddress")
                .UseMetadata("routeRepresentation", "/users/{userId}/primaryAddress")
                .UseTarget<UserAddressVertex>();

            vertex.HasEdge("profile")
                .UseMetadata("routeRepresentation", "/users/{userId}/profile")
                .UseTarget<UserProfileVertex>();


            // Define Operations
            vertex.HasQuery("GetUsers")
                .UseResolver(async (context, cancellationToken) =>
                {
                    return default;
                });

            vertex.HasQuery("GetUserById")
                .UseRoute("{userId}")
                .UseResolver(async (context, cancellationToken) =>
                {
                    return default;
                }); 
            
        });
        builder.AddVertex<UserAddress>(vertex =>
        {

        });
        builder.AddVertex<UserProfile>(vertex =>
        {

        });

        builder.AddQuery("GetUsers")
            .UseNode("users")
            .UseRoute("/users")
            .UseResolver(async (context, cancellationToken) =>
            {
                return default;
            });
    });
    //.AddOGraph("Employees", builder =>
    //{
    //})
    //.AddOGraph("Organization", builder =>
    //{

    //});

var app = builder.Build();

app.UseOGraph();
app.Run();
