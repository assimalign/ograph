using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphProperty : IOGraphProperty
{
    public OGraphProperty()
    {
        this.Metadata = new OGraphMetadata();
        this.Middleware = new OGraphPropertyMiddlewareQueue();
    }
    public Name Name { get; set; }
    public IOGraphType Type { get; set; } 
    public IOGraphMetadata Metadata { get; }
    public IOGraphPropertyResolver Resolver { get; set; } 
    public IOGraphPropertyMiddlewareQueue Middleware { get; }
    public Task<IOGraphResult> ExecuteAsync(IOGraphPropertyContext context, CancellationToken cancellationToken = default)
    {
        return Middleware.BuildHandlerChain(Resolver)
            .Invoke(context, cancellationToken)
            .AsTask();
    }
}