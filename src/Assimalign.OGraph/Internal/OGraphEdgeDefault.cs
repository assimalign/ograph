using System.Linq;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace Assimalign.OGraph.Internal;

internal class OGraphEdgeDefault : IOGraphEdge
{
    private int chainIndex;

    public OGraphEdgeDefault()
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
    public IOGraphQueryProvider QueryProvider { get; set; }
    public OGraphQueryOptions QueryOptions { get; set; }

    public Task<IOGraphResult> ExecuteAsync(IOGraphEdgeContext context, CancellationToken cancellationToken = default)
    {
        return Middleware.BuildHandlerChain(Resolver).Invoke(context, cancellationToken);
    }
}
