using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

public sealed class GdmDateType : GdmScalarType<DateOnly>
{
    public GdmDateType() { }

    public override Label Label => "Date";

    public override DateOnly Read(ref Utf8JsonReader reader)
    {
        return DateOnly.Parse(reader.GetString()!);
    }
    public override DateOnly Read(XmlReader reader)
    {
        return DateOnly.Parse(reader.ReadContentAsString());
    }
    public override void Write(Utf8JsonWriter writer, DateOnly value)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
    }
    public override void Write(XmlWriter writer, DateOnly value)
    {
        writer.WriteValue(value.ToString("yyyy-MM-dd"));
    }
}