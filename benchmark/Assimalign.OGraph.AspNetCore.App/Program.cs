using Assimalign.OGraph;
using Assimalign.OGraph.AspNetCore;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddOGraph("Users", builder =>
{
    builder.AddNode("users")
        .UseMetadata("description", "")
        .UseType<UserType>();


    builder.AddNode("addresses")
        .UseMetadata("", "");

    builder.AddEdge("userAddresses")
        .UseMetadata("description", "Returns a collection of addresses associated with a single user object.")
        .UseSourceNode("users")
        .UseTargetNode("addresses")
        .UseResolver(context =>
        {
            var parent = context.GetParent<User>();


            return default;
        });

    builder.AddOperation("GetUsers")
        .UseMethod("GET")
        .UseRoute("/api/users")
        .UseNode("users")
        .UseMiddleware((context, next) =>
        {
            Console.WriteLine("Middleware 1 Invoked");

            return next.Invoke(context);
        })
        .UseMiddleware((context, next) =>
        { 
            var claimsPrincipal = context.GetClaimsPrincipal();
            
            if (!claimsPrincipal.HasClaim("role", "some.role"))
            {
                return default;
            }

            Console.WriteLine("Middleware 2 Invoked");

            return next.Invoke(context);
        })

        // Queryable Return Type
        .UseResolver(context =>
        {
            
            var list = new List<User>()
            {
                new User
                {
                    FirstName = "Chase",
                    LastName = "Crawford"
                },
                new User
                {
                    FirstName = "John",
                    LastName = "Doe"
                },
                new User
                {
                    FirstName = "Jane",
                    LastName = "Doe"
                }
            };

            return list.AsQueryable();
        });
});

var app = builder.Build();

app.UseOGraph();

app.Run();
