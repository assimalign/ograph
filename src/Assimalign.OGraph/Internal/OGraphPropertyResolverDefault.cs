using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;



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
    public ValueTask<IOGraphPropertyResult> InvokeAsync(IOGraphPropertyContext context)
    {
        return resolver.Invoke(context);
    }
}
