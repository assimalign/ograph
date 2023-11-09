using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class PropertyBindingResolver : IOGraphPropertyBindingResolver
{
    private readonly OGraphPropertyResolver resolver;
    public PropertyBindingResolver(OGraphPropertyResolver resolver)
    {
        this.resolver = resolver;
    }
    public ValueTask<IOGraphResult> InvokeAsync(IOGraphPropertyBindingResolverContext context, CancellationToken cancellationToken = default)
    {
        return resolver.Invoke(context, cancellationToken);
    }
}