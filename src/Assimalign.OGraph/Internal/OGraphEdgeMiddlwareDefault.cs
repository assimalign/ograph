using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphEdgeMiddlwareDefault : IOGraphEdgeMiddleware
{
    private readonly OGraphEdgeMiddleware middleware;

    public OGraphEdgeMiddlwareDefault(OGraphEdgeMiddleware middleware)
    {
        this.middleware = middleware;   
    }
    public Task<IOGraphEdgeResult> InvokeAsync(IOGraphEdgeResolverContext context, OGraphEdgeHandler next)
    {
        return middleware.Invoke(context, next);
    }
}
