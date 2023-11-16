using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Gdm;
using System.Text.Json;

internal class Executor : IOGraphExecutor
{
    private readonly OGraphExecutorOptions options;
    private readonly IEnumerable<IOGraphGdm> models;

    public Executor(IEnumerable<IOGraphGdm> models, OGraphExecutorOptions options)
    {
        this.options = options;
        this.models = models;
#if DEBUG
        Log();
#endif

    }


#if DEBUG
    private void Log()
    {
        foreach (var model in models)
        {
            foreach (var vertex in model.GetGdmVertices())
            {
                foreach (var binding in vertex.Bindings.OfType<IOGraphOperationBinding>())
                {
                    switch (binding.Method)
                    {
                        case "GET":
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("GET    ");
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine(binding.Route.ToString());

                                break;
                            }
                        case "PUT":
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write("PUT    ");
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine(binding.Route.ToString());
                                break;
                            }
                        case "POST":
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("POST   ");
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine(binding.Route.ToString());
                                break;
                            }
                        case "DELETE":
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("DELETE ");
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine(binding.Route.ToString());
                                break;
                            }
                    }
                }
            }
        }
    }


#endif

    public Task ExecuteAsync(IOGraphExecutorContext context, CancellationToken cancellationToken)
    {
        try
        {
            using var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            // Check if timeout was set
            if (options.Timeout > options.Timeout)
            {
                cancellationTokenSource.CancelAfter(options.Timeout);
            }

            // Throw an exception for cancellation
            cancellationTokenSource.Token.ThrowIfCancellationRequested();

            var response = context.Response;
            var request = context.Request;

            foreach (var model in models)
            {
                foreach (var vertex in model.GetGdmVertices())
                {
                    foreach (var binding in vertex.Bindings.OfType<IOGraphOperationBinding>())
                    {
                        if (binding.Method.Equals(request.Method) && binding.Route.IsMatch(request.Path))
                        {
                            //if (request.Headers.TryGetValue("Content-Type", out var contentType))
                            //{

                            //}
                            return binding.ExecuteAsync(new OperationBindingContext()
                            {
                                Element = vertex,
                                Request = request,
                                Response = response,
                                Writer = new Utf8JsonWriter(response.Body),
                                ServiceProvider = options.ServiceProvider!

                            }, cancellationTokenSource.Token);
                        }
                    }
                }
            }


            return Task.CompletedTask;
        }
        catch (Exception exception)
        {
            throw;
        }
    }
}
