using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph;

internal class OperationBindingResolver : IOGraphOperationBindingResolver
{
    private readonly OGraphOperationBindingResolver resolver;
    public OperationBindingResolver(OGraphOperationBindingResolver resolver)
    {
        this.resolver = resolver;
    }
    public Task<IOGraphResult> InvokeAsync(IOGraphOperationBindingContext context, CancellationToken cancellationToken = default)
    {
        return resolver.Invoke(context, cancellationToken);
    }
}
