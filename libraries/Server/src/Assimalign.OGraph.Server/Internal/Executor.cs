using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Gdm;

internal class Executor : IOGraphExecutor
{
    IOGraphGdm Model { get; } = default!;



    public Task ExecuteAsync(IOGraphExecutorContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            using var cancellationTokenSource = new CancellationTokenSource();

            var response = context.Response;
            var request = context.Request;

            foreach (var vertex in Model.GetGdmVertices())
            {
                foreach (var binding in vertex.Bindings.OfType<IOGraphOperationBinding>())
                {
                    if (binding.Method.Equals(request.Method) && binding.Route.IsMatch(request.Path))
                    {
                        if (request.Headers.TryGetValue("Content-Type", out var contentType))
                        {

                        }
                        return binding.ExecuteAsync(new OperationBindingContext()
                        {
                            Element = vertex,
                            Request = request,
                            Response = response
                        });
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


    private void Temp()
    {
        var descriptor = default(IOGraphOperationBindingDescriptor)!;

        descriptor.UseName("QueryUsers")
            .UseQueryParam("")
            .UseRoute("https://users/{userId}")
            .UseMiddleware((context, cancellationToken, next) =>
            {
                return next.Invoke(context, cancellationToken);
            })
            .UseResolver((context, cancellationToken) =>
            {
                return default;
            });
    }
}
