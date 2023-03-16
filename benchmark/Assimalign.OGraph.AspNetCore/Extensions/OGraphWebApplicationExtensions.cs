
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Assimalign.OGraph.AspNetCore;

using Assimalign.OGraph.Execution;


public static class OGraphWebApplicationExtensions
{
    public static WebApplication UseOGraph(this WebApplication app)
    {
        var graph = app.Services.GetService<IOGraph>();


        if (graph is null)
        {
            throw new Exception("");
        }
        if (!graph.Operations.Any())
        {
            throw new Exception("");
        }

        var routes = graph.GetRoutes();



        foreach (var operation in graph.Operations.OrderBy(x => x.Route))
        {
            if (app.Environment.IsDevelopment())
            {
                var max = graph.Operations.Select(x => x.Name.Value).Max(x => x.Length);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(operation.Name.Value.PadRight(max + 1, ' '));

                switch (operation.Method)
                {
                    case "GET":
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        }
                    case "DELETE":
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        }
                    case "PUT":
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                        }
                    case "POST":
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            break;
                        }
                }

                Console.Write(operation.Method.Value.PadRight(8, ' '));

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(operation.Route);
                Console.Write(Environment.NewLine);
            }
            app.MapMethods(operation.Route, new string[] { operation.Method }, async context =>
            {
                try
                {
                    var graphExecutor = context.RequestServices.GetRequiredService<IOGraphHttpExecutor>();

                    var graphResponse = await graphExecutor.ExecuteAsync(operation.Name, new OGraphRequest(context.Request));

                    context.Response.StatusCode = graphResponse.StatusCode;

                    context.Response.ContentType = "application/json";

                    if (graphResponse.Body.Length > 0)
                    {
                        await graphResponse.Body.CopyToAsync(context.Response.Body);
                    }
                }
                catch (Exception exception)
                {
                    context.Response.StatusCode = 500;
                    //context.Response.Body.C
                }

            }).WithDisplayName(operation.Name);
        }


        return app.UseReservedRoutes();
    }

    // This will register the reserver routes supported by ograph
    private static WebApplication UseReservedRoutes(this WebApplication app)
    {
        app.MapGet("/$graph", async context =>
        {
            var graph = app.Services.GetService<IOGraph>();
        });

        return app;
    }
}
