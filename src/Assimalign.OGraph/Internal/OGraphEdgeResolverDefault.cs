using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphEdgeResolverDefault : IOGraphEdgeResolver
{
    private readonly OGraphEdgeResolver resolver;

    public OGraphEdgeResolverDefault(OGraphEdgeResolver resolver)
    {
        this.resolver = resolver;
    }
    public Task<IOGraphEdgeResult> InvokeAsync(IOGraphEdgeResolverContext context)
    {
        return resolver.Invoke(context);
    }
}
