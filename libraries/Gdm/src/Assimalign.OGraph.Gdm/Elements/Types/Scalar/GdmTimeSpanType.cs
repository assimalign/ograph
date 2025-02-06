using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

public sealed class GdmTimeSpanType : GdmScalarType<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader)
    {
        return TimeSpan.Parse(reader.GetString()!);
    }
    public override TimeSpan Read(XmlReader reader)
    {
        return TimeSpan.Parse(reader.ReadContentAsString());
    }
    public override void Write(Utf8JsonWriter writer, TimeSpan value)
    {
        writer.WriteStringValue(value.ToString());
    }
    public override void Write(XmlWriter writer, TimeSpan value)
    {
        writer.WriteValue(value.ToString());
    }
}
