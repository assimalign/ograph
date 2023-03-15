using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    public Task<IOGraphOperationResult> InvokeAsync(IOGraphOperationContext context)
    {
        return resolver.Invoke(context);
    }
}
