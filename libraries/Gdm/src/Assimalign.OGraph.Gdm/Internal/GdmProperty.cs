using System;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm = {Label} Property")]
internal class GdmProperty : IOGraphGdmProperty
{
    public GdmProperty()
    {
        Metadata = new GdmMetadata();
    }

    public Label Label { get; set; }
    public PropertyInfo PropertyInfo { get; set; } = default!;
    public IOGraphGdmTypeReference Type { get; set; } = default!;
    public IOGraphGdmTypeReference DeclaringType { get; set; } = default!;
    public IOGraphGdmMetadata Metadata { get; init; }
    public bool IsComputed { get; set; }
    public bool IsNullable { get; set; } = true;
    public bool IsReadOnly { get; set; }
    public GdmPropertyGetter Getter { get; set; } = default!;
    public GdmPropertySetter Setter { get; set; } = default!;
    public GdmElementKind ElementKind => GdmElementKind.Property;
    public IEnumerable<IOGraphGdmBinding> Bindings { get; } = new List<IOGraphGdmBinding>();
    public void Bind(IOGraphGdmBinding binding)
    {
        if (binding is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(binding));
        }
        if (this.HasBinding(binding.Label))
        {
            ThrowHelper.ThrowInvalidOperationException($"The element already contains a binding with the label: {binding.Label}");
        }
        (Bindings as List<IOGraphGdmBinding>)!.Add(binding);
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

    public static GdmProperty Wrap(IOGraphGdmProperty property)
    {
        if (property is GdmProperty prop)
        {
            return prop;
        }

        return new GdmProperty()
        {
            IsComputed = property.IsComputed,
            Getter = property.Getter,
            Setter = property.Setter,
            IsNullable = property.IsNullable,
            Metadata = property.Metadata,
            Label = property.Label,
            Type = property.Type
        };
    }

    public void Unbind(IOGraphGdmBinding binding)
    {
        (Bindings as List<IOGraphGdmBinding>)!.Remove(binding);
    }
}