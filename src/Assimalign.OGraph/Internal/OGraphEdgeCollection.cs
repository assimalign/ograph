using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.OGraph.Internal;

internal class OGraphEdgeCollection : List<IOGraphEdge>,
    IOGraphEdgeCollection
{
    public bool TryAddEdge(IOGraphEdge edge)
    {
        foreach (var item in this)
        {
            if (item.Label == edge.Label)
            {

            }
        }

        Add(edge);

        return true;
    }

    public bool TryGetEdge(Name name, out IOGraphEdge? edge)
    {
        throw new NotImplementedException();
    }
}
