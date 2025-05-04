using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Assimalign.OGraph.Gdm.Internal;

namespace Assimalign.OGraph.Gdm.Elements;

public class GdmMemberCollection : IOGraphGdmMemberCollection, IEnumerable<GdmMember>
{
    private readonly List<GdmMember> _items;
    private bool _isReadOnly;

    public GdmMemberCollection()
    {
        _items = new List<GdmMember>();
    }

    public int Count => _items.Count;
    public bool IsReadOnly => _isReadOnly;
    public GdmMember this[GdmName name]
    {
        get
        {
            GdmMember? member = default;

            for (int i = 0; i < _items.Count; i++)
            {
                if ((member = _items[i]).Name == name)
                {
                    return member;
                }
            }

            throw new KeyNotFoundException();
        }
    }
    public void Add(GdmMember member)
    {
        switch (member)
        {
            case GdmProperty property:
                Add(property);
                break;

            case GdmFunction function:
                break;

            default:
                throw new ArgumentException("");
        }
    }
    public void Add(GdmProperty property)
    {
        ThrowHelper.ThrowIfNull(property);

        for (int i = 0; i < _items.Count; i++)
        {
            var member = _items[i];

            // 
            if (object.ReferenceEquals(property, member))
            {
                return;
            }

            if (member is GdmProperty prop && prop.Name == property.Name)
            {
                Remove(prop);
            }
        }

        _items.Add(property);
    }
    public void Remove(GdmMember member)
    {
        switch (member)
        {
            case GdmProperty property:
                Remove(property);
                break;

            case GdmFunction function:
                Remove(function);
                break;

            default:
                throw new ArgumentException("");
        }
    }
    public void Remove(GdmProperty property)
    {

    }
    public void Remove(GdmFunction function)
    {

    }
    public void Clear()
    {
        _items.Clear();
    }
    public bool TryGetProperty(GdmName name, out GdmProperty property)
    {
        property = default;



        return true;
    }
    public IEnumerator<GdmMember> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IOGraphGdmMember IOGraphGdmMemberCollection.this[GdmName name] => this[name];
    void IOGraphGdmMemberCollection.Add(IOGraphGdmMember member)
    {
        Add(ThrowHelper.ThrowIfNotType<GdmMember>(member));
    }
    void IOGraphGdmMemberCollection.Remove(IOGraphGdmMember member)
    {
        Remove(ThrowHelper.ThrowIfNotType<GdmMember>(member));
    }
    void IOGraphGdmMemberCollection.Clear()
    {
        Clear();
    }
    IEnumerator<IOGraphGdmMember> IEnumerable<IOGraphGdmMember>.GetEnumerator()
    {
        return GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
