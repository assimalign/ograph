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
public class GdmNodeCollection : IOGraphGdmNodeCollection, IEnumerable<GdmNode>
{
    private bool _isReadOnly;
    private readonly List<GdmNode> _items;

    public GdmNodeCollection()
    {
        _items = new List<GdmNode>();
    }

    public int Count => _items.Count;
    public bool IsReadOnly => _isReadOnly;
    public GdmNode this[GdmName name]
    {
        get
        {
            for (int i = 0; i < _items.Count; i++)
            {
                var item = _items[i];

                if (item.Name == name)
                {
                    return item;
                }
            }

            throw new KeyNotFoundException();
        }
    }
    public void Add(GdmNode item)
    {
        ThrowHelper.ThrowIfNull(item);

        for (int i = 0; i < _items.Count; i++)
        {
            var vertex = _items[i];

            if (ReferenceEquals(item, vertex))
            {
                return;
            }

            if (vertex.Name == item.Name)
            {
                _items.RemoveAt(i);
                break;
            }
        }

        _items.Add(item);
    }
    public void Remove(GdmNode item)
    {
        ThrowHelper.ThrowIfNull(item);

        bool isRemoved = _items.Remove(item);

        if (!isRemoved)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Name == item.Name)
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
    public IEnumerator<GdmNode> GetEnumerator()
    {
        return _items.GetEnumerator();
    }
    IOGraphGdmNode IOGraphGdmNodeCollection.this[GdmName name] => this[name];
    void IOGraphGdmNodeCollection.Add(IOGraphGdmNode vertex)
    {
        Add(ThrowHelper.ThrowIfNotType<GdmNode>(vertex));
    }
    void IOGraphGdmNodeCollection.Remove(IOGraphGdmNode vertex)
    {
        Remove(ThrowHelper.ThrowIfNotType<GdmNode>(vertex));
    }
    IEnumerator<IOGraphGdmNode> IEnumerable<IOGraphGdmNode>.GetEnumerator()
    {
        return GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
