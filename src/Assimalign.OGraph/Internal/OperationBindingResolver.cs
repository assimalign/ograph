using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph;

internal class OperationBindingResolver : IOGraphOperationBindingResolver
{
    private readonly OGraphOperationResolver resolver;
    public OperationBindingResolver(OGraphOperationResolver resolver)
    {
        this.resolver = resolver;
    }
    public Task<IOGraphResult> InvokeAsync(IOGraphOperationBindingResolverContext context, CancellationToken cancellationToken = default)
    {
        return resolver.Invoke(context, cancellationToken);
    }
}
