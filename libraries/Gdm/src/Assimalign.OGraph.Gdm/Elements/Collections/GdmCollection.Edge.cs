using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;


[DebuggerDisplay("Count = {Count}")]
public class GdmEdgeCollection : IOGraphGdmEdgeCollection, IEnumerable<GdmEdge>
{
    private readonly List<GdmEdge> _items;
    private bool _isReadOnly;

    public GdmEdgeCollection()
    {
        _items = new List<GdmEdge>();
    }

    public int Count => _items.Count;
    public bool IsReadOnly => _isReadOnly;
    public void Add(GdmEdge edge)
    {

    }
    public void Remove(GdmEdge edge)
    {

    }
    public void Clear()
    {
        _items.Clear();
    }
    public IEnumerator<GdmEdge> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

  //  IOGraphGdmEdge IOGraphGdmEdgeCollection.this[GdmLabel label] => throw new NotImplementedException();
    void IOGraphGdmEdgeCollection.Add(IOGraphGdmEdge edge)
    {
        Add(ThrowHelper.ThrowIfNotType<GdmEdge>(edge));
    }
    void IOGraphGdmEdgeCollection.Remove(IOGraphGdmEdge edge)
    {
        Remove(ThrowHelper.ThrowIfNotType<GdmEdge>(edge));
    }
    void IOGraphGdmEdgeCollection.Clear()
    {
        Clear();
    }
    IEnumerator<IOGraphGdmEdge> IEnumerable<IOGraphGdmEdge>.GetEnumerator()
    {
        return GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
