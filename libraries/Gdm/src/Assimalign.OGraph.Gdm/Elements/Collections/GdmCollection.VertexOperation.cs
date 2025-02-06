using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Elements;

using Assimalign.OGraph.Gdm.Internal;

public class GdmVertexOperationCollection : List<IOGraphGdmVertexOperation>, IOGraphGdmOperationCollection
{
    private bool isReadOnly;

    public GdmVertexOperationCollection()
    {
        
    }


    public IOGraphGdmVertexOperation this[Label label] => this.OfType<IOGraphGdmVertexOperation>().Find(label);
    IOGraphGdmOperation IOGraphGdmOperationCollection.this[Label label]
    {
        get
        {
            return this[label];
        }
    }
    bool ICollection<IOGraphGdmOperation>.IsReadOnly => isReadOnly;


    public new void Add(IOGraphGdmVertexOperation item)
    {
        (this as ICollection<IOGraphGdmOperation>).Add(item);
    }
    void ICollection<IOGraphGdmOperation>.Add(IOGraphGdmOperation item)
    {
        AssertReadOnly();
        base.Add(AssertType(item));
    }


    public new bool Remove(IOGraphGdmVertexOperation item)
    {
        return (this as ICollection<IOGraphGdmOperation>).Remove(item);
    }
    bool ICollection<IOGraphGdmOperation>.Remove(IOGraphGdmOperation item)
    {
        AssertReadOnly();
        return base.Remove(AssertType(item));
    }


    public new void Clear()
    {
        (this as ICollection<IOGraphGdmOperation>).Clear();
    }
    void ICollection<IOGraphGdmOperation>.Clear()
    {
        AssertReadOnly();
        base.Clear();
    }

    public new bool Contains(IOGraphGdmVertexOperation item)
    {
        return (this as ICollection<IOGraphGdmOperation>).Contains(item);
    }
    bool ICollection<IOGraphGdmOperation>.Contains(IOGraphGdmOperation item)
    {
        AssertReadOnly();
        return base.Contains(AssertType(item));
    }


    void ICollection<IOGraphGdmOperation>.CopyTo(IOGraphGdmOperation[] array, int arrayIndex)
    {
        base.CopyTo(AssertTypes(array), arrayIndex);
    }
    IEnumerator<IOGraphGdmOperation> IEnumerable<IOGraphGdmOperation>.GetEnumerator()
    {
        return base.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return base.GetEnumerator();
    }
    private void AssertReadOnly()
    {
        if ((this as ICollection<IOGraphGdmOperation>).IsReadOnly)
        {
            ThrowHelper.ThrowInvalidOperationException("");
        }
    }
    private IOGraphGdmVertexOperation AssertType(IOGraphGdmOperation item)
    {
        if (item is not IOGraphGdmVertexOperation)
        {
            ThrowHelper.ThrowInvalidOperationException("");
        }
        return (IOGraphGdmVertexOperation)item;
    }
    private IOGraphGdmVertexOperation[] AssertTypes(IOGraphGdmOperation[] array)
    {
        if (array is not IOGraphGdmVertexOperation[])
        {
            ThrowHelper.ThrowInvalidOperationException("");
        }
        return (IOGraphGdmVertexOperation[])array;
    }
}
