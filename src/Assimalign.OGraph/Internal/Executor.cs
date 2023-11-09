
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



    public async Task<IOGraphExecutorResponse> ExecuteAsync(IOGraphExecutorRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            using var cancellationTokenSource = new CancellationTokenSource();

            var response = new ExecutorResponse();

            foreach (var vertex in Model.GetGdmVertices())
            {
                var bindings = vertex.GetBindings()
                    .OfType<IOGraphOperationBinding>();

                foreach (var binding in bindings)
                {
                    if (binding.Method.Equals(request.Method) && binding.Route.IsMatch(request.Path))
                    {
                        await binding.ExecuteAsync(new OperationBindingContext()
                        {
                            Element = vertex,
                            Request = request,
                            Response = response
                        });

                        return response;
                    }
                }
            }

            return response;
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
