using Assimalign.OGraph;
using Assimalign.OGraph.AspNetCore;
using Assimalign.OGraph.Execution;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddOGraph("Users", graphBuilder =>
{
    graphBuilder.AddNode<User>("users", descriptor =>
    {
        
    });
    graphBuilder.AddOperation("GetUsers", descriptor =>
    {
        descriptor.UseMethod("GET")
            .UseRoute("/api/users")
            .UseNodes("users")
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
    graphBuilder.AddOperation("CreateUser", descriptor =>
    {
        descriptor.UseMethod("POST")
            .UseRoute("/api/users")
            .UseNodes("users")
            .UseResolver(async context =>
            {
                return new OGraphOperationResult<User>()
                {
                    Data = new User()
                    {
                        FirstName = "Chase"
                    }
                };
            });
    });
    graphBuilder.AddOperation("UpdateUser", descriptor =>
    {
        descriptor.UseMethod("PUT")
            .UseRoute("/api/users/{userId}")
            .UseNodes("users")
            .UseResolver(async context =>
            {
                return new OGraphOperationResult<User>()
                {
                    Data = new User()
                    {
                        FirstName = "Chase"
                    }
                };
            });
    });
    graphBuilder.AddOperation("DeleteUser", descriptor =>
    {
        descriptor.UseMethod("DELETE")
            .UseRoute("/api/users/{userId}")
            .UseNodes("users")
            .UseResolver(async context =>
            {
                return new OGraphOperationResult<User>()
                {
                    Data = new User()
                    {
                        FirstName = "Chase"
                    }
                };
            });
    });
});

var app = builder.Build();

app.UseOGraph();

app.Run();
