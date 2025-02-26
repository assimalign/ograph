using System;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public class GdmProperty : GdmMember, IOGraphGdmProperty
{
    public GdmProperty(
        GdmName label, 
        GdmType type, 
        GdmType declaringType,
        bool isReadOnly = false,
        bool isNullable = false) 
        : base(label, declaringType)
    {
        Type = ThrowHelper.ThrowIfNull(type, nameof(type));
        IsReadOnly = isReadOnly;
        IsNullable = isNullable;
    }

    public GdmProperty(
        GdmName label,
        GdmType type,
        GdmType declaringType,
        GdmPropertyGetter getter,
        GdmPropertySetter setter,
        bool isReadOnly = false,
        bool isNullable = false) 
        : this(label, type, declaringType, isReadOnly, isNullable)
    {
        Getter = ThrowHelper.ThrowIfNull(getter, nameof(getter)); 
        Setter = ThrowHelper.ThrowIfNull(setter, nameof(setter));
    }

    public GdmType Type { get; }
    public GdmPropertyGetter Getter { get; internal set; } = default!;
    public GdmPropertySetter Setter { get; internal set; } = default!;
    public bool IsReadOnly { get; internal set; }
    public bool IsNullable { get; internal set; }
    public override GdmElementKind ElementKind { get; } = GdmElementKind.Property;
    IOGraphGdmType IOGraphGdmProperty.Type => Type;
}