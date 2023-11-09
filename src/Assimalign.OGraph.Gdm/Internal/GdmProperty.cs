using System;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm Property: {Label}")]
internal class GdmProperty : IOGraphGdmProperty
{
    private readonly IList<IOGraphGdmBinding> bindings = new List<IOGraphGdmBinding>();
    
    public GdmProperty()
    {
        Metadata = new GdmMetadata();
    }

    public Label Label { get; set; }
    public PropertyInfo PropertyInfo { get; set; } = default!;
    public IOGraphGdmTypeReference Type { get; set; } = default!;
    public IOGraphGdmTypeReference DeclaringType { get; set; } = default!;
    public IOGraphGdmMetadata Metadata { get; init; }
    public bool IsKey { get; set; }
    public bool IsComputed { get; set; }
    public bool IsNullable { get; set; } = true;
    public GdmPropertyGetter Getter { get; set; } = default!;
    public GdmPropertySetter Setter { get; set; } = default!;
    public GdmElementType ElementType => GdmElementType.Property;
    public void AddBinding(IOGraphGdmBinding binding)
    {
        if (binding is null)
        {
            throw new ArgumentNullException(nameof(binding));
        }
        bindings.Add(binding);
    }
    public IEnumerable<IOGraphGdmBinding> GetBindings()
    {
        return bindings;
    }
    public override string ToString()
    {
        return Label;
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