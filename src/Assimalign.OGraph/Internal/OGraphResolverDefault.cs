using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphResolverDefault<T> : IOGraphResolver<T>
{
    private readonly OGraphResolver<T> resolver;
    
    public OGraphResolverDefault(OGraphResolver<T> resolver)
    {
        this.resolver = resolver;
    }

    public Task<T> ResolveAsync(IOGraphResolverContext context) => this.resolver.Invoke(context);
    async Task<object> IOGraphResolver.ResolveAsync(IOGraphResolverContext context) => await ResolveAsync(context);
}
