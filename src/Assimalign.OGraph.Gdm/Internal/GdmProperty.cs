using System;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm Property: {Name}")]
internal class GdmProperty : IOGraphGdmProperty
{
    private readonly IList<IOGraphGdmPropertyBinding> bindings = new List<IOGraphGdmPropertyBinding>();
    public GdmProperty()
    {
        Metadata = new GdmMetadata();
    }

    public Label Name { get; set; }
    public PropertyInfo PropertyInfo { get; set; }
    public IOGraphGdmTypeReference Type { get; set; } = default!;
    public IOGraphGdmMetadata Metadata { get; init; }
    public bool IsKey { get; set; }
    public bool IsComputed { get; set; }
    public bool IsNullable { get; set; }
    public GdmPropertyGetter Getter { get; set; } = default!;
    public GdmPropertySetter Setter { get; set; } = default!;
    public void AddBinding(IOGraphGdmPropertyBinding binding)
    {
        if (binding is null)
        {
            throw new ArgumentNullException(nameof(binding));
        }
        bindings.Add(binding);
    }
    public IEnumerable<IOGraphGdmPropertyBinding> GetBindings()
    {
        return bindings;
    }
    public override string ToString()
    {
        return Name;
    }
    public override int GetHashCode()
    {
        return PropertyInfo.GetHashCode();
    }
    public override bool Equals(object? obj)
    {
        if (obj is GdmProperty property)
        {
            return PropertyInfo.Equals(property.PropertyInfo);
        }
        return false;
    }
}