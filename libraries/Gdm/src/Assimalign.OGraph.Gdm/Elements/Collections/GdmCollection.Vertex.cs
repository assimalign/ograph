using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

[DebuggerDisplay("Count = {Count}")]
public class GdmVertexCollection : IOGraphGdmVertexCollection, IEnumerable<GdmVertex>
{
    private bool _isReadOnly;
    private readonly List<GdmVertex> _items;

    public GdmVertexCollection()
    {
        _items = new List<GdmVertex>();
    }

    public int Count => _items.Count;
    public bool IsReadOnly => _isReadOnly;
    public GdmVertex this[GdmLabel label]
    {
        get
        {
            for (int i = 0; i < _items.Count; i++)
            {
                var item = _items[i];

                if (item.Label == label)
                {
                    return item;
                }
            }

            throw new KeyNotFoundException();
        }
    }
    public void Add(GdmVertex item)
    {
        ThrowHelper.ThrowIfNull(item);

        for (int i = 0; i < _items.Count; i++)
        {
            var vertex = _items[i];

            if (ReferenceEquals(item, vertex))
            {
                return;
            }

            if (vertex.Label == item.Label)
            {
                _items.RemoveAt(i);
                break;
            }
        }

        _items.Add(item);
    }
    public void Remove(GdmVertex item)
    {
        ThrowHelper.ThrowIfNull(item);

        bool isRemoved = _items.Remove(item);

        if (!isRemoved)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Label == item.Label)
                {
                    _items.RemoveAt(i);
                }
            }
        }
    }
    public void Clear()
    {
        _items.Clear();
    }
    public IEnumerator<GdmVertex> GetEnumerator()
    {
        return _items.GetEnumerator();
    }
    IOGraphGdmVertex IOGraphGdmVertexCollection.this[GdmLabel label] => this[label];
    void IOGraphGdmVertexCollection.Add(IOGraphGdmVertex vertex)
    {
        Add(ThrowHelper.ThrowIfNotType<GdmVertex>(vertex));
    }
    void IOGraphGdmVertexCollection.Remove(IOGraphGdmVertex vertex)
    {
        Remove(ThrowHelper.ThrowIfNotType<GdmVertex>(vertex));
    }
    IEnumerator<IOGraphGdmVertex> IEnumerable<IOGraphGdmVertex>.GetEnumerator()
    {
        return GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
