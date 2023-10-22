
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Assimalign.OGraph.AspNetCore;


public static class OGraphWebApplicationExtensions
{
    public static WebApplication UseOGraph(this WebApplication app)
    {
        var graph = app.Services.GetService<IOGraph>();
        var options = app.Services.GetService<IOptions<OGraphOptions>>().Value;

        if (graph is null)
        {
            throw new Exception("");
        }
        if (!graph.Operations.Any())
        {
            throw new Exception("");
        }

        var routes = graph.GetRoutes().ToList();

        foreach (var operation in graph.Operations)
        {
            if (app.Environment.IsDevelopment())
            {
                var max = graph.Operations.Select(x => x.Label.Value).Max(x => x.Length);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(operation.Label.Value.PadRight(max + 1, ' '));

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
            app.MapMethods(string.Join('/', options.RoutePrefix, operation.Route), new string[] { operation.Method }, async context =>
            {
                try
                {
                    var graphExecutor = context.RequestServices.GetRequiredService<IOGraphExecutor>();

                    var graphResponse = await graphExecutor.ExecuteAsync(new OGraphRequest(context.Request));

                    context.Response.StatusCode = graphResponse.StatusCode;
                    context.Response.ContentType = "application/json";

                    if (graphResponse.Body.Length > 0)
                    {
                        graphResponse.Body.Position = 0;
                        await graphResponse.Body.CopyToAsync(context.Response.Body);
                    }
                }
                catch (Exception exception)
                {
                    context.Response.StatusCode = 500;
                    //context.Response.Body.C
                }

            }).WithDisplayName(operation.Label);
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
