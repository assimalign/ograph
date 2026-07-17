using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OperationBindingResolverWrapper : IOGraphOperationBindingResolver
{
    private readonly OGraphOperationBindingResolver resolver;
    public OperationBindingResolverWrapper(OGraphOperationBindingResolver resolver)
    {
        this.resolver = resolver;
    }
    public Task<IOGraphResult> InvokeAsync(IOGraphOperationBindingContext context, CancellationToken cancellationToken = default)
    {
        return resolver.Invoke(context, cancellationToken);
    }
}
