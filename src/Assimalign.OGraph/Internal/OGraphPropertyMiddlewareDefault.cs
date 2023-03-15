using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphPropertyMiddlewareDefault : IOGraphPropertyMiddleware
{
    private readonly OGraphPropertyMiddleware middleware;

    public OGraphPropertyMiddlewareDefault(OGraphPropertyMiddleware middleware)
    {
        this.middleware = middleware;
    }

    public ValueTask<IOGraphPropertyResult> InvokeAsync(IOGraphPropertyContext context, OGraphPropertyHandler next)
    {
        return middleware.Invoke(context, next);
    }
}
