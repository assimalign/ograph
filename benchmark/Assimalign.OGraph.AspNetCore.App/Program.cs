using Assimalign.OGraph;
using Assimalign.OGraph.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRepository<User>, UserRepository>();

builder.Services.AddOGraph("Users", builder =>
{
    builder.AddNode<UserNode>();
    //builder.AddNode<UserAddressNode>();
    //builder.AddNode<CompanyNode>();
    //builder.AddNode<CompanyAddressNode>();
    //builder.AddNode<EmployeeNode>();
    //builder.AddNode<EmployeeAddressNode>();

    //builder.AddOperation<UserCreateOperation>();


    builder.AddOperation("GetUsers")
        .UseMethod("GET")
        .UseRoute("/api/users")
        .UseQueryOptions(options =>
        {
            options.CanFilter = false;
            options.CanPage = false;
        })
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
                
            }

            Console.WriteLine("Middleware 2 Invoked");

            return await next.Invoke(context);
        })
        .UseResolver(async context =>
        {
            return context.GetService<IRepository<User>>()
                .Queryable;
        });
});

var app = builder.Build();

app.UseOGraph();
app.Run();
