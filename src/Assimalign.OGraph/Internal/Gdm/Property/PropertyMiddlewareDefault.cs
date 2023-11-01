using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class PropertyMiddlewareDefault : IOGraphPropertyMiddleware
{
    private readonly OGraphPropertyMiddleware middleware;

    public PropertyMiddlewareDefault(OGraphPropertyMiddleware middleware)
    {
        this.middleware = middleware;
    }

    public ValueTask<IOGraphResult> InvokeAsync(IOGraphPropertyContext context, OGraphPropertyHandler next)
    {
        return middleware.Invoke(context, next);
    }
}
