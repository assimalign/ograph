using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphTypeCollection : List<IOGraphType>,
    IOGraphTypeCollection
{
    public bool TryAdd(IOGraphType type)
    {
        throw new NotImplementedException();
    }

    public bool TryGet(Name name, out IOGraphType type)
    {
        throw new NotImplementedException();
    }
}
