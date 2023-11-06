using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmPropertyCollection : IOGraphGdmPropertyCollection
{
    private readonly HashSet<IOGraphGdmProperty> properties;

    public GdmPropertyCollection()
    {
        properties = new(new GdmPropertyComparer());
    }

    public IOGraphGdmProperty this[Label name] => properties.First(p=>p.Name == name);
    public int Count => properties.Count;
    public bool IsReadOnly { get; set; }
    public void Add(IOGraphGdmProperty item)
    {
        AssertReadOnly();
        AssertNotNull(item);
        properties.Add(item);
    }
    public void Clear()
    {
        AssertReadOnly();
        properties.Clear();
    }
    public bool Contains(IOGraphGdmProperty item)
    {
        AssertNotNull(item);
        return properties.Contains(item);
    }
    public void CopyTo(IOGraphGdmProperty[] array, int arrayIndex)
    {
        properties.ToArray().CopyTo(array, arrayIndex);
    }
    public bool Remove(IOGraphGdmProperty item)
    {
        AssertReadOnly();
        AssertNotNull(item);
        return properties.Remove(item);
    }
    public IEnumerator<IOGraphGdmProperty> GetEnumerator()
    {
        return properties.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    private void AssertReadOnly()
    {
        if (IsReadOnly)
        {
            throw new InvalidOperationException("The collection is readonly.");
        }
    }
    private void AssertNotNull(object item)
    {
        if (item is null)
        {
            throw new ArgumentNullException(nameof(item));
        }
    }

    private class GdmPropertyComparer : IEqualityComparer<IOGraphGdmProperty>
    {
        public bool Equals(IOGraphGdmProperty? left, IOGraphGdmProperty? right)
        {
            return left.Name == right.Name;
        }

        public int GetHashCode(IOGraphGdmProperty obj)
        {
            if (obj is GdmProperty ip)
            {
                return ip.GetHashCode();
            }
            return obj.Name.GetHashCode();
        }
    }
}