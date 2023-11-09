using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;


[DebuggerDisplay("Count = {Count}")]
internal class GdmEdgeReferenceCollection : IOGraphGdmEdgeReferenceCollection
{
    public int Count => throw new NotImplementedException();

    public bool IsReadOnly => throw new NotImplementedException();

    public void Add(IOGraphGdmEdgeReference item)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(IOGraphGdmEdgeReference item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(IOGraphGdmEdgeReference[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<IOGraphGdmEdgeReference> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public bool Remove(IOGraphGdmEdgeReference item)
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
