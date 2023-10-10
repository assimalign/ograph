using Assimalign.OGraph;
using Assimalign.OGraph.AspNetCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IRepository<User>, UserRepository>();

builder.Services
    .AddOGraphOptions(options =>
    {
        options.RoutePrefix = "api";
    })
    .AddOGraph("Users", builder =>
    {
        // Composite Implementation
        builder.AddNode<UserNode>();
        builder.AddNode<UserAddressNode>();
        builder.AddNode<CompanyNode>();
        builder.AddNode<CompanyAddressNode>();
        builder.AddNode<EmployeeNode>();
        builder.AddNode<EmployeeAddressNode>();

        //builder.AddOperation<UserCreateOperation>();

        // Fluent Implementation
        builder.AddCommand("GetUsers")
            .UseMethod(Method.Post)
            .UseRoute("/users")
            .UseNode<UserNode>()
            .UseTestResolver(async (context, token) =>
            {
                var value = context.GetQuery();

                if (value is null)
                {
                    return default(IOGraphError);
                }

              

                return new User();

            });
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
            //.UseEitherResolver(context =>
            //{
            //    var value = context.GetNode();

            //    if (value is null)
            //    {
            //        return new QueryableResult();
            //    }

            //    return context.GetService<IRepository<User>>()
            //        .Queryable;
            //});


        builder.AddCommand("UpdateUser")
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
