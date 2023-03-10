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
        this.resolver = resolver;
    }
    public async ValueTask<IOGraphPropertyResult> InvokeAsync(IOGraphPropertyResolverContext context, CancellationToken cancellationToken = default)
    {
        var result = await resolver.Invoke(context);

        return new OGraphPropertyResult(result);
    }
}
internal class OGraphPropertyResolverDefault : IOGraphPropertyResolver
{
    private readonly OGraphPropertyResolver resolver;

    public OGraphPropertyResolverDefault(OGraphPropertyResolver resolver)
    {
        this.resolver = resolver;
    }
    public ValueTask<IOGraphPropertyResult> InvokeAsync(IOGraphPropertyResolverContext context, CancellationToken cancellationToken = default)
    {
        return resolver.Invoke(context);
    }
}
