using System;
using System.Reflection;
using System.Linq.Expressions;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public class GdmProperty : GdmMember, IOGraphGdmProperty
{
    internal GdmProperty(
        GdmName name,
        GdmType type,
        GdmType declaringType,
        GdmPropertyGetter getter,
        GdmPropertySetter setter,
        bool isReadOnly = false,
        bool isNullable = false) 
        : base(name, declaringType)
    {
        Type = ThrowHelper.ThrowIfNull(type);
        Getter = ThrowHelper.ThrowIfNull(getter); 
        Setter = ThrowHelper.ThrowIfNull(setter);
        IsReadOnly = isReadOnly;
        IsNullable = isNullable;
    }

    public GdmType Type { get; }
    public GdmPropertyGetter Getter { get; }
    public GdmPropertySetter Setter { get; }
    public bool IsReadOnly { get; }
    public bool IsNullable { get;  }
    public override GdmElementKind ElementKind { get; } = GdmElementKind.Member;
    IOGraphGdmType IOGraphGdmProperty.Type => Type;
}