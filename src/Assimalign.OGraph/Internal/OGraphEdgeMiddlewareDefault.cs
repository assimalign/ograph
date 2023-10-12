using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphEdgeMiddlewareDefault : IOGraphEdgeMiddleware
{
    private readonly OGraphEdgeMiddleware middleware;

    public OGraphEdgeMiddlewareDefault(OGraphEdgeMiddleware middleware)
    {
        this.middleware = middleware;   
    }
    public Task<IOGraphResult> InvokeAsync(IOGraphEdgeContext context, OGraphEdgeHandler next)
    {
        return middleware.Invoke(context, next);
    }
}
