using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

[DebuggerDisplay("Count = {Count}")]
public class GdmMemberCollection : IOGraphGdmMemberCollection, IEnumerable<GdmMember>
{
    private readonly List<GdmMember> _items;
    private bool _isReadOnly;

    public GdmMemberCollection()
    {
        _items = new List<GdmMember>();
    }

    public GdmMemberCollection(bool isReadOnly) : this()
    {
        _isReadOnly = isReadOnly;
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

    public GdmProperty GetProperty(GdmName name)
    {
        if (this[name] is not GdmProperty property)
        {
            throw new KeyNotFoundException();
        }

        return property;
    }

    public GdmFunction GetFunction(GdmName name)
    {
        if (this[name] is not GdmFunction function)
        {
            throw new KeyNotFoundException();
        }

        return function;
    }
    
    public void Add(GdmMember member)
    {
        AssertReadOnly();

        switch (member)
        {
            case GdmProperty property:
                Add(property);
                break;

            case GdmFunction function:
                Add(function);
                break;

            default:
                throw new ArgumentException("");
        }
    }
    public void Add(GdmProperty property)
    {
        ThrowHelper.ThrowIfNull(property);

        AssertReadOnly();

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
    public void Add(GdmFunction function)
    {
        AssertReadOnly();
    }
    public void Remove(GdmMember member)
    {
        AssertReadOnly();

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
        AssertReadOnly();
    }
    public void Remove(GdmFunction function)
    {
        AssertReadOnly();
    }
    public void Clear()
    {
        AssertReadOnly();
        _items.Clear();
    }
    public IEnumerator<GdmMember> GetEnumerator()
    {
        return _items.GetEnumerator();
    }
    private void AssertReadOnly()
    {
        if (_isReadOnly)
        {
            ThrowHelper.ThrowInvalidOperationException("The collection is readonly.");
        }
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
