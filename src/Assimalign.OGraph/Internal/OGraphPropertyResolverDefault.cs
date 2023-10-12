using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

// A wrapper class for OGraphPropertyResolver callback
internal class OGraphPropertyResolverDefault : IOGraphPropertyResolver
{
    private readonly OGraphPropertyResolver resolver;

    public OGraphPropertyResolverDefault(OGraphPropertyResolver resolver)
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
