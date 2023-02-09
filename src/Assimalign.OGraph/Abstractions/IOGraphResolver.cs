using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphResolver
{
    Task<object> ResolveAsync(IOGraphResolverContext context);    
}

public interface IOGraphResolver<T> : IOGraphResolver
{
    Task<T> ResolveAsync(IOGraphResolverContext context);
}