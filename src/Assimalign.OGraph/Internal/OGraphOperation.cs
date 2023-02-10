using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal abstract class OGraphOperation : IOGraphOperation
{
    public Name Name { get; set; }
    public Route Route { get; set; }
    public Method Method { get; set; }

    public Task OnResolveAsync(IOGraphResolverContext context)
    {
        throw new NotImplementedException();
    }
}
