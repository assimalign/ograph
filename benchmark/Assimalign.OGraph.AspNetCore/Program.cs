using Assimalign.OGraph;
using Assimalign.OGraph.AspNetCore;
using Assimalign.OGraph.Execution;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddOGraph("Users", graph =>
{
    graph.AddNode<User>("users", descriptor =>
    {
        
    });

    graph.AddOperation("GetUsers", descriptor =>
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
                        }
                    }
                };
            });
    });
});

var app = builder.Build();

foreach (var operation in app.Services.GetRequiredService<IOGraph>().Operations)
{
    app.MapMethods(operation.Route, new string[] { operation.Method }, async context =>
    {
        var executor = context.RequestServices.GetRequiredService<IOGraphExecutor>();
        var response = await executor.ExecuteAsync(operation.Name, new OGraphHttpRequest(context.Request));

        context.Response.StatusCode = response.StatusCode;

        if (response.Body.Length > 0)
        {
            await response.Body.CopyToAsync(context.Response.Body);
        }
    });
}


app.Run();
