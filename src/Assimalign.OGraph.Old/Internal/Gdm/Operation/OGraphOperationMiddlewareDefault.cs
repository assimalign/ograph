using System;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphOperationMiddlewareDefault : IOGraphOperationMiddleware
{
    private readonly OGraphOperationMiddleware middleware;

    public OGraphOperationMiddlewareDefault(OGraphOperationMiddleware middleware)
    {
        if (middleware is null)
        {
            throw new ArgumentNullException(nameof(middleware));
        }
        this.middleware = middleware;
    }

    public Task<IOGraphResult> InvokeAsync(IOGraphOperationContext context, OGraphOperationHandler next)
    {
        return middleware.Invoke(context, next);
    }
}
