using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

[DebuggerDisplay("Count = {Count}")]
public class GdmGraphCollection : IOGraphGdmGraphCollection, IEnumerable<GdmGraph>
{
    private bool _isReadOnly;
    private readonly List<GdmGraph> _items;

    public GdmGraphCollection()
    {
        _items = new List<GdmGraph>();
    }

    public int Count => _items.Count;
    public bool IsReadOnly => _isReadOnly;
    public GdmGraph Find(GdmName name)
    {
        return _items.First(p => p.Name == name);
    }
    public void Add(GdmGraph item)
    {
        AssertIsReadOnly();

        var graph = ThrowHelper.ThrowIfNull(item);

        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].Name == graph.Name)
            {
                ThrowHelper.ThrowInvalidOperationException($"The model already contains graph: {graph.Name}");
            }
        }

        _items.Add(graph);
    }
    public void Remove(GdmGraph item)
    {
        AssertIsReadOnly();

        var graph = ThrowHelper.ThrowIfNull(item, nameof(item));

        bool isRemoved = _items.Remove(graph);

        if (isRemoved)
        {
            return;
        }

        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].Name == graph.Name)
            {
                _items.RemoveAt(i);
            }
        }
    }
    public void Clear()
    {
        AssertIsReadOnly();
        _items.Clear();
    }
    public IEnumerator<GdmGraph> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    private void AssertIsReadOnly()
    {
        if (IsReadOnly)
        {
            ThrowHelper.ThrowInvalidOperationException("");
        }
    }


    void IOGraphGdmGraphCollection.Remove(IOGraphGdmGraph item)
    {
        Remove(ThrowHelper.ThrowIfNotType<GdmGraph>(item));
    }
    void IOGraphGdmGraphCollection.Add(IOGraphGdmGraph item)
    {
        Add(ThrowHelper.ThrowIfNotType<GdmGraph>(item));
    }
    void IOGraphGdmGraphCollection.Clear()
    {
        Clear();
    }
    IEnumerator<IOGraphGdmGraph> IEnumerable<IOGraphGdmGraph>.GetEnumerator()
    {
        return GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}