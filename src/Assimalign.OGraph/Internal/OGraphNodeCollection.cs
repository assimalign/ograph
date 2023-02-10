using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphNodeCollection : IOGraphNodeCollection
{
    public IOGraphNode this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public int Count => throw new NotImplementedException();

    public bool IsReadOnly => throw new NotImplementedException();

    public void Add(IOGraphNode item)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(IOGraphNode item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(IOGraphNode[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<IOGraphNode> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public int IndexOf(IOGraphNode item)
    {
        throw new NotImplementedException();
    }

    public void Insert(int index, IOGraphNode item)
    {
        throw new NotImplementedException();
    }

    public bool Remove(IOGraphNode item)
    {
        throw new NotImplementedException();
    }

    public void RemoveAt(int index)
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
