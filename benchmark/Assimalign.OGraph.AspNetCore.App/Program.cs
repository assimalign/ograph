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
    graphBuilder.AddOperation("GetUsers")
        .UseMethod("GET")
        .UseRoute("/api/users")
        //.UseNodes("users")
        .UseMiddleware((context, next) =>
        {
            Console.WriteLine("Middleware 1 Invoked");

            return next(context);
        })
        .UseMiddleware((context, next) =>
        {
            Console.WriteLine("Middleware 2 Invoked");

            return next(context);
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
