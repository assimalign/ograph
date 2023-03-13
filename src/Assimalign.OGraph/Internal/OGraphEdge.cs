using System.Linq;
using System;

namespace Assimalign.OGraph.Internal;

internal class OGraphEdge : IOGraphEdge
{
    private int chainIndex;

    public OGraphEdge()
    {
        this.Metadata = new OGraphMetadata();
        this.Middleware = new OGraphEdgeMiddlewareQueue();
    }

    public Name Name { get; set; }
    public IOGraphNode Source { get; set; }
    public IOGraphNode Target { get; set; }
    public IOGraphMetadata Metadata { get; }
    public IOGraphEdgeResolver Resolver { get; set; }
    public IOGraphEdgeMiddlewareQueue Middleware { get; }

    public OGraphEdgeHandler GetResolverChain()
    {
        var memoise = Cacher<OGraphEdge, OGraphEdgeHandler>.Memoise(edge =>
        {
            if (edge.Resolver is null)
            {
                throw new Exception();
            }
            return GetResolverChain(new OGraphEdgeHandler(edge.Resolver.InvokeAsync));
        });

        return memoise.Invoke(this);
    }
    private OGraphEdgeHandler GetResolverChain(OGraphEdgeHandler handler)
    {
        var middleware = Middleware.Reverse().Skip(chainIndex).First();
        var next = new OGraphEdgeHandler(context =>
        {
            return middleware.InvokeAsync(context, handler);
        });
        if (chainIndex < Middleware.Count - 1)
        {
            chainIndex++;
            return GetResolverChain(next);
        }
        chainIndex = 0;
        return next;
    }
}
