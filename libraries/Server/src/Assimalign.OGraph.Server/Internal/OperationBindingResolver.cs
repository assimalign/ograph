using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph;

internal class OperationBindingResolver : IOGraphOperationBindingResolver
{
    private readonly OGraphGdmVertexResolver resolver;
    public OperationBindingResolver(OGraphGdmVertexResolver resolver)
    {
        this.resolver = resolver;
    }
    public Task<IOGraphResult> InvokeAsync(IOGraphOperationBindingResolverContext context, CancellationToken cancellationToken = default)
    {
        return resolver.Invoke(context, cancellationToken);
    }
}
