using System;

namespace Assimalign.OGraph.Gdm.Elements;

public abstract class GdmEnumType : GdmType, IOGraphGdmEnumType
{
    internal GdmEnumType() { }
    internal GdmEnumType(GdmName name, GdmGraph graph) : base(name, graph) { }
    public abstract GdmEnumValue[] Values { get; }
    public sealed override GdmTypeKind Kind { get; } = GdmTypeKind.Enum;
    public static GdmEnumType<TEnum> Create<TEnum>(GdmGraph graph) where TEnum : struct, Enum
    {
        return new GdmEnumType<TEnum>(graph);
    }
    public static GdmEnumType<TEnum> Create<TEnum>(GdmName name, GdmGraph graph) where TEnum : struct, Enum
    {
        return new GdmEnumType<TEnum>(name, graph);
    }
}
