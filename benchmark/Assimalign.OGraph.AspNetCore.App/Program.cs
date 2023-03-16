using Assimalign.OGraph;
using Assimalign.OGraph.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOGraph("Users", builder =>
{
    builder.AddNode<UserNode>();
    builder.AddNode<UserAddressNode>();
    builder.AddNode<CompanyNode>();
    builder.AddNode<CompanyAddressNode>();
    builder.AddNode<EmployeeNode>();
    builder.AddNode<EmployeeAddressNode>();


    builder.AddOperation<UserCreateOperation>();

    builder.AddOperation("GetUsers")
        .UseMethod("GET")
        .UseRoute("/api/users")
        .UseNode<UserNode>()
        .UseMiddleware((context, next) =>
        {
            Console.WriteLine("Middleware 1 Invoked");

            return next.Invoke(context);
        })
        .UseMiddleware(async (context, next) =>
        { 
            var claimsPrincipal = context.GetClaimsPrincipal();
            
            if (!claimsPrincipal.HasClaim("role", "some.role"))
            {
                return new OGraphErrorResult(new OGraphError()
                {

                });
            }

            Console.WriteLine("Middleware 2 Invoked");

            return await next.Invoke(context);
        })
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
