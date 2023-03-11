using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphPropertyResolverDefault<T> : IOGraphPropertyResolver
{
    private readonly OGraphPropertyResolver<T> resolver;

    public OGraphPropertyResolverDefault(OGraphPropertyResolver<T> resolver)
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }

        this.resolver = resolver;
    }
    public async ValueTask<IOGraphPropertyResult> InvokeAsync(IOGraphPropertyResolverContext context, CancellationToken cancellationToken = default)
    {
        var result = await resolver.Invoke(context);


        return new OGraphPropertyResult(result);
    }
}