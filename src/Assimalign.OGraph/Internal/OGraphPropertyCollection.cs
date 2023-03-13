using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


namespace Assimalign.OGraph.Internal;

internal class OGraphPropertyCollection : IOGraphPropertyCollection
{
    private readonly IList<IOGraphProperty> properties;
    public OGraphPropertyCollection()
    {
        this.properties = new List<IOGraphProperty>();
    }
    public int Count => this.properties.Count;

    public bool IsReadOnly { get; set; }

    public void Add(IOGraphProperty property)
    {
        AssertReadOnly();

        if( property is null)
        {
            throw new ArgumentNullException(nameof(property));
        }
        if (this.properties.Any(x=>x.Name == property.Name))
        {
            throw new ArgumentException($"The property with the name '{property.Name}' already exists.");
        }
        this.properties.Add(property);
    }
    public void Remove(IOGraphProperty property)
    {
        AssertReadOnly();

        if (property is null)
        {
            throw new ArgumentNullException(nameof(property));
        }

        this.properties.Remove(property);
    }
    public bool TryGet(Name name, out IOGraphProperty? property)
    {
        property = this.properties.FirstOrDefault(p => p.Name == name);

        return property is null ? false : true;
    }

    public IEnumerator<IOGraphProperty> GetEnumerator() => this.properties.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    private void AssertReadOnly()
    {
        if (IsReadOnly)
        {
            throw new InvalidOperationException("The Collection is ReadOnly.");
        }
    }
}
