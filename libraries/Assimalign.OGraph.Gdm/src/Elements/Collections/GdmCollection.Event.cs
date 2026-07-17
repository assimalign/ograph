using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public sealed class GdmEventCollection : IOGraphGdmEventCollection
{
    public int Count => throw new NotImplementedException();
    public bool IsReadOnly => throw new NotImplementedException();

    public void Add(IOGraphGdmEvent item)
    {
        throw new NotImplementedException();
    }

    public void Remove(IOGraphGdmEvent item)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public IEnumerator<IOGraphGdmEvent> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
