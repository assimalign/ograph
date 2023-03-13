using Assimalign.OGraph;
using Assimalign.OGraph.AspNetCore;
using Assimalign.OGraph.Execution;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddOGraph("Users", builder =>
{
    builder.AddNode("users")
        .UseMetadata("description", "");
    //.UseType<User>(descriptor =>
    //{
    //    descriptor.HasProperty(p => p.Details)
    //        .UseName("UserDetails")
    //        .UseType(descriptor =>
    //        {
    //            descriptor.HasProperty("test")
    //                .UseResolver(context =>
    //                {
    //                    return string.Empty;
    //                });

    //            descriptor.HasProperty(p => p.Ssn)
    //                .UseName("socialSecurityNumber")
    //                .UseResolver(context =>
    //                {
    //                    return string.Empty;
    //                });
    //        });
    //});

    //builder.AddEdge("userAddresses")
    //    .UseMetadata("description", "Returns a collection of addresses associated with a single user object.")
    //    .UseSourceNode("users")
    //    .UseTargetNode("addresses")
    //    .UseResolver(context =>
    //    {
    //        var parent = context.GetParent<User>();


    //        return default;
    //    });


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
            Console.WriteLine("Middleware 2 Invoked");

            return next.Invoke(context);
        })
        .UseResolver(async context =>
        {


            return new OGraphOperationResult<User[]>()
            {
                StatusCode = 200,
                Data = new User[]
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
                }
            };
        });
});

var app = builder.Build();

app.UseOGraph();

app.Run();
