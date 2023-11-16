using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OperationBindingMiddleware : IOGraphOperationBindingMiddleware
{
    private readonly OGraphOperationBindingMiddleware middleware;
    public OperationBindingMiddleware(OGraphOperationBindingMiddleware middleware)
    {
        this.middleware = middleware;
    }
    public Task<IOGraphResult> InvokeAsync(IOGraphOperationBindingContext context, CancellationToken cancellationToken, OGraphOperationBindingMiddlewareHandler next)
    {
        return middleware.Invoke(context, cancellationToken, next);
    }
}
