using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmDateTimeOffsetType : GdmScalarType<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader)
    {
        return reader.GetDateTimeOffset();
    }
    public override DateTimeOffset Read(XmlReader reader)
    {
        return reader.ReadContentAsDateTimeOffset();
    }
    public override void Write(Utf8JsonWriter writer, DateTimeOffset value)
    {
        writer.WriteStringValue(value);
    }
    public override void Write(XmlWriter writer, DateTimeOffset value)
    {
        writer.WriteValue(value);
    }
}