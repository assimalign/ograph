using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphEdgeResolverDefault : IOGraphEdgeResolver
{
    private readonly OGraphEdgeResolver resolver;

    public OGraphEdgeResolverDefault(OGraphEdgeResolver resolver)
    {
        this.resolver = resolver;
    }
    public Task<IOGraphResult> InvokeAsync(IOGraphEdgeContext context, CancellationToken cancellationToken = default)
    {
        return resolver.Invoke(context, cancellationToken);
    }
}
