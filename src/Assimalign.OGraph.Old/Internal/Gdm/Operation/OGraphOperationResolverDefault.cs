using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphOperationResolverDefault : IOGraphOperationResolver
{
    private readonly OGraphOperationResolver resolver;

    public OGraphOperationResolverDefault(OGraphOperationResolver resolver)
    {
        this.resolver = resolver;
    }

    public Task<IOGraphResult> InvokeAsync(IOGraphOperationContext context, CancellationToken cancellationToken = default)
    {
        return resolver.Invoke(context, cancellationToken);
    }
}