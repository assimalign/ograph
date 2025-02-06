using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

public sealed class GdmTimeType : GdmScalarType<TimeOnly>
{
    public GdmTimeType() { }
    public override Label Label => "Time";
    public override TimeOnly Read(ref Utf8JsonReader reader)
    {
        return TimeOnly.Parse(reader.GetString()!);
    }
    public override TimeOnly Read(XmlReader reader)
    {
        return TimeOnly.Parse(reader.ReadContentAsString());
    }
    public override void Write(Utf8JsonWriter writer, TimeOnly value)
    {
        writer.WriteStringValue(value.ToString());
    }
    public override void Write(XmlWriter writer, TimeOnly value)
    {
        writer.WriteValue(value.ToString());
    }
}