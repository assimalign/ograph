using System;
using System.Linq;

namespace Assimalign.OGraph.Internal;

internal class OGraphOperation : IOGraphOperation
{
    private int chainIndex;

    public OGraphOperation()
    {
        this.Metadata = new OGraphMetadata();
        this.Middleware = new OGraphOperationMiddlewareQueue();
    }

    public Name Name { get; set; }
    public Route Route { get; set; }
    public Method Method { get; set; }
    public bool IsEnabled { get; set; }
    public IOGraphNode? Node { get; set; }
    public IOGraphType? RequestType { get; set; }
    public IOGraphType? ResponseType { get; set; }
    public IOGraphOperationResolver? Resolver { get; set; }
    public IOGraphOperationMiddlewareQueue Middleware { get; }
    public IOGraphMetadata Metadata { get; }
    public IOGraphQueryProvider QueryProvider { get; set; }
    public OGraphOperationHandler GetResolverChain()
    {
        var memoise = Cacher<OGraphOperation, OGraphOperationHandler>.Memoise(operation =>
        {
            if (operation.Resolver is null)
            {
                throw new Exception();
            } 
            return GetResolverChain(new OGraphOperationHandler(operation.Resolver.InvokeAsync));
        });

        return memoise.Invoke(this);
    }
    private OGraphOperationHandler GetResolverChain(OGraphOperationHandler handler)
    {
        var middleware = Middleware.Reverse().Skip(chainIndex).First();
        var next = new OGraphOperationHandler(context =>
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
