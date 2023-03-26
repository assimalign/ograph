using Assimalign.OGraph;
using Assimalign.OGraph.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRepository<User>, UserRepository>();

builder.Services
    .AddOGraphOptions(options =>
    {
        options.RoutePrefix = "api";
    })
    .AddOGraph("Users", builder =>
    {
        builder.AddNode<UserNode>();
        builder.AddNode<UserAddressNode>();
        builder.AddNode<CompanyNode>();
        builder.AddNode<CompanyAddressNode>();
        builder.AddNode<EmployeeNode>();
        builder.AddNode<EmployeeAddressNode>();

        //builder.AddOperation<UserCreateOperation>();

        builder.AddOperation("GetUsers")
            .UseMethod(Method.Get)
            .UseRoute("/users")
            .UseQueryOptions(options =>
            {
                options.CanFilter = false;
                options.CanPage = false;
            })
            .UseNode<UserNode>()
            //.UseMiddleware((context, next) =>
            //{
            //    Console.WriteLine("Middleware 1 Invoked");

            //    return next.Invoke(context);
            //})
            //.UseMiddleware(async (context, next) =>
            //{ 
            //    var claimsPrincipal = context.GetClaimsPrincipal();

            //    if (!claimsPrincipal.Identity.IsAuthenticated)
            //    {
            //        return OGraphResult
            //            .Unauthorzed()
            //            .Build();
            //    }

            //    return await next.Invoke(context);
            //})
            .UseResolver(async context =>
            {
                return context.GetService<IRepository<User>>()
                    .Queryable;
            });


        builder.AddOperation("UpdateUser")
            .UseMethod(Method.Put)
            .UseRoute("/users/{userId:Guid}")
            .UseNode<UserNode>()
            .UseResolver(async context =>
            {
                return default;
            });
    });

var app = builder.Build();

app.UseOGraph();
app.Run();
