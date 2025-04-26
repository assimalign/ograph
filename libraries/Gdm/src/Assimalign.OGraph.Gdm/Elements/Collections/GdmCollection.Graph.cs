using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public class GdmGraphCollection : IOGraphGdmGraphCollection
{
    private readonly List<GdmGraph> items;

    public GdmGraphCollection()
    {
        items = new List<GdmGraph>();
    }

    public int Count => items.Count;
    public bool IsReadOnly { get; }
    public void Add(GdmGraph item)
    {
        items.Add(ThrowHelper.ThrowIfNull(item, nameof(item)));
    }
    public void Clear()
    {
        items.Clear();
    }

    void ICollection<IOGraphGdmGraph>.Add(IOGraphGdmGraph item)
    {
        ThrowHelper.ThrowIfNull(item, nameof(item));
        Add(ThrowHelper.ThrowIfNotType<GdmGraph>(item));
    }
    bool ICollection<IOGraphGdmGraph>.Contains(IOGraphGdmGraph item)
    {
        throw new NotImplementedException();
    }
    void ICollection<IOGraphGdmGraph>.CopyTo(IOGraphGdmGraph[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }
   
    
    IEnumerator<IOGraphGdmGraph> IEnumerable<IOGraphGdmGraph>.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    bool ICollection<IOGraphGdmGraph>.Remove(IOGraphGdmGraph item)
    {
        throw new NotImplementedException();
    }
}