using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public sealed class GdmStringType : GdmScalarType<string>
{
    public GdmStringType() { }
    public GdmStringType(GdmGraph graph) : base(graph) { }

    public override GdmPrimitiveType PrimitiveType => GdmPrimitiveType.String;
    public override string Read(ref Utf8JsonReader reader)
    {
        return reader.GetString()!;
    }
    public override string Read(XmlReader reader)
    {
        return reader.ReadContentAsString();
    }
    public override void Write(Utf8JsonWriter writer, string value)
    {
        writer.WriteStringValue(value);
    }
    public override void Write(XmlWriter writer, string value)
    {
        writer.WriteValue(value);
    }
    public override string Parse(string? value)
    {
        return value!;
    }
    public override bool TryParse(string? value, out string result)
    {
        return (result = value!) is not null;
    }
}
