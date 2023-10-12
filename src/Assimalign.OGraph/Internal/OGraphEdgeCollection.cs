using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.OGraph.Internal;

internal class OGraphEdgeCollection : List<IOGraphEdge>,
    IOGraphEdgeCollection
{
    public bool TryGetEdge(Name name, out IOGraphEdge? edge)
    {
        throw new NotImplementedException();
    }
}
