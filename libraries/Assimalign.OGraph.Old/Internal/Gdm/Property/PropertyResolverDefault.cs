using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

// A wrapper class for OGraphPropertyResolver callback
internal class PropertyResolverDefault : IOGraphPropertyResolver
{
    private readonly OGraphPropertyResolver resolver;

    public PropertyResolverDefault(OGraphPropertyResolver resolver)
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver)); 
        }

        this.resolver = resolver;
    }
    public ValueTask<IOGraphResult> InvokeAsync(IOGraphPropertyContext context, CancellationToken cancellationToken = default)
    {
        return resolver.Invoke(context, cancellationToken);
    }
}
