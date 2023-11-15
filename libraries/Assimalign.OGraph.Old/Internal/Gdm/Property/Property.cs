using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class Property : IOGraphProperty
{
    public Property()
    {
        this.Metadata = new Metadata();
        this.Middleware = new PropertyMiddlewareQueue();
    }
    public Label Name { get; set; }
    public bool IsKey { get; set; }
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