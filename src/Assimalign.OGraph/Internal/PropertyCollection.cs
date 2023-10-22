using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Internal;

internal class PropertyCollection : HashSet<IOGraphProperty>,
    IOGraphPropertyCollection
{
    private static readonly IEqualityComparer<IOGraphProperty> comparer = new PropertyComparer();
    public PropertyCollection() 
        : base(comparer) { }

    public bool IsReadOnly { get; set; }
    public new void Add(IOGraphProperty property)
    {
        AssertReadOnly();
        base.Add(property);
    }
    public new void Remove(IOGraphProperty property)
    {
        AssertReadOnly();
        base.Remove(property);
    }
    public bool TryGet(Name name, out IOGraphProperty? property)
    {
        property = this.FirstOrDefault(p => p.Name == name);
        return property is null ? false : true;
    }
    private void AssertReadOnly()
    {
        if (IsReadOnly)
        {
            throw new InvalidOperationException("The Collection is ReadOnly.");
        }
    }
    private class PropertyComparer : IEqualityComparer<IOGraphProperty>
    {
        public bool Equals(IOGraphProperty? left, IOGraphProperty? rigth)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] IOGraphProperty obj)
        {
            throw new NotImplementedException();
        }
    }
}