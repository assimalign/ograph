using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public sealed class GdmCharType : GdmValueScalarType<char>
{
    public GdmCharType(GdmGraph graph) : base(graph) { }

    public override GdmPrimitiveType PrimitiveType { get; } = GdmPrimitiveType.String;
    public override char Read(ref Utf8JsonReader reader)
    {
        var value = reader.GetString();

        if (value is null || value.Length == 0 || value.Length > 1)
        {
            throw new JsonException("");
        }

        return value[0];
    }
    public override char Read(XmlReader reader)
    {
        return reader.ReadElementContentAsString()[0];
    }
    public override void Write(Utf8JsonWriter writer, char value)
    {
        //writer.WriteBooleanValue(value);
    }
    public override void Write(XmlWriter writer, char value)
    {
        //writer.WriteValue(value);
    }

    public override char Parse(string? value)
    {
        return char.Parse(value!);
    }
    public override bool TryParse(string? value, out char result)
    {
        return char.TryParse(value, out result);
    }
}
