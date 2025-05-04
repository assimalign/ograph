using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm.Elements;

public class GdmComplexType : GdmType,
    IOGraphGdmComplexType
{
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    private readonly Type _runtimeType = default!;
    private readonly GdmName _name;


    internal GdmComplexType() { }

    public GdmComplexType([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type runtimeType, GdmGraph graph)
       : this(runtimeType.Name, runtimeType, graph)
    {
    }

    public GdmComplexType(GdmName name, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type runtimeType, GdmGraph graph)
        : base(graph)
    {
        _name = name;
        _runtimeType = runtimeType;
    }

    public sealed override GdmName Name => _name;
    public sealed override Type RuntimeType => _runtimeType;
    public GdmMemberCollection Members { get; } = new GdmMemberCollection();
    public sealed override GdmTypeKind Kind { get; } = GdmTypeKind.Entity;
    IOGraphGdmMemberCollection IOGraphGdmComplexType.Members => Members;


    public override object Read(XmlReader reader)
    {
        return base.Read(reader);
    }

    public override object Read(ref Utf8JsonReader reader)
    {
        return base.Read(ref reader);
    }

    public override void Write(Utf8JsonWriter writer, object value)
    {
        base.Write(writer, value);
    }

    public override void Write(XmlWriter writer, object value)
    {
        base.Write(writer, value);
    }
}
