using System;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmProperty : GdmMember, IOGraphGdmProperty
{
    public PropertyInfo PropertyInfo { get; set; } = default!;
    public IOGraphGdmTypeReference Type { get; set; } = default!;
    public bool IsNullable { get; set; } = true;
    public bool IsReadOnly { get; set; }
    public GdmPropertyGetter Getter { get; set; } = default!;
    public GdmPropertySetter Setter { get; set; } = default!;
    public override GdmElementKind ElementKind { get; } = GdmElementKind.Property;
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
            Getter = property.Getter,
            Setter = property.Setter,
            IsNullable = property.IsNullable,
            Label = property.Label,
            Type = property.Type
        };
    }
}