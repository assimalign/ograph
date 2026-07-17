using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Gdm;
using Assimalign.OGraph.Syntax;
using System.Text.Json;

internal class Executor : IOGraphExecutor
{
    private readonly OGraphExecutorOptions options;
    private readonly IEnumerable<IOGraphGdm> models;

    public Executor(IEnumerable<IOGraphGdm> models, OGraphExecutorOptions options)
    {
        this.options = options;
        this.models = models;
    }

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
                        // 1. Match method and route
                        if (binding.Method.Equals(request.Method) && binding.Route.IsMatch(request.Path))
                        {
                            

                            // 2. 415 Check - Let's check Content Length & Type header
                            if (request.Headers.TryGetValue(HeaderKey.ContentLength, out var contentLength))
                            {
                                var length = long.Parse(contentLength!);
                                if (length > 0 && request.Headers.TryGetValue(HeaderKey.ContentType, out var contentType))
                                {
                                    var collection = (contentType as ICollection<string>);

                                    if (!collection.Contains(OGraphMediaType.Json) && collection.Contains(OGraphMediaType.Xml))
                                    {

                                    }
                                }
                                // Unsupported Media Type
                                else
                                {

                                }
                            }

                            // 3. 406 Check - Check for accept header
                            if (request.Headers.TryGetValue(HeaderKey.Accept, out var accept))
                            {
                                var collection = accept as ICollection<string>;

                                // Accepts either any content-type or both OGraph content-type.
                                if (collection.Contains("*/*") || (collection.Contains(OGraphMediaType.Xml) && collection.Contains(OGraphMediaType.Json)))
                                {

                                }
                                else if (collection.Contains(OGraphMediaType.Xml))
                                {

                                }
                                else if (collection.Contains(OGraphMediaType.Json))
                                {

                                }
                                // The user requested an Unsupported media type. - 406 (Not Acceptable)
                                else
                                {
                                   // return ProcessErrorResultAsJsonAsync(new OGraph)
                                }
                            }
                            return binding.ExecuteAsync(new OperationBindingContext()
                            {
                                Element = vertex,
                                Request = request,
                                Response = response,
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
