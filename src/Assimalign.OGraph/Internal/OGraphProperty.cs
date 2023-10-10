using System;
using System.Linq;

namespace Assimalign.OGraph.Internal;

internal class OGraphProperty : IOGraphProperty
{
    private int chainIndex;

    public OGraphProperty()
    {
        this.Metadata = new OGraphMetadata();
        this.Middleware = new OGraphPropertyMiddlewareQueue();
    }


    public Name Name { get; set; }
    public IOGraphType? Type { get; set; } 
    public IOGraphMetadata Metadata { get; }
    public IOGraphPropertyResolver Resolver { get; set; } 
    public IOGraphPropertyMiddlewareQueue Middleware { get; }
    public OGraphPropertyHandler BuildHandlerChain()
    {
        var memoise = Cacher<OGraphProperty, OGraphPropertyHandler>.Memoise(property =>
        {
            if (property.Resolver is null)
            {
                throw new Exception();
            }
            var root = new OGraphPropertyHandler(property.Resolver.InvokeAsync);

            if (Middleware.Count == 0)
            {
                return root;
            }

            return GetResolverChain(root);
        });

        return memoise.Invoke(this);
    }
    private OGraphPropertyHandler GetResolverChain(OGraphPropertyHandler handler)
    {
        var middleware = Middleware.Reverse().Skip(chainIndex).First();
        var next = new OGraphPropertyHandler(context =>
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
