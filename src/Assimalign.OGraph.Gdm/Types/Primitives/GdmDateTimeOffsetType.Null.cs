using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmNullDateTimeOffsetType : GdmPrimitiveType<DateTimeOffset?>
{
    public override DateTimeOffset? Read(ref Utf8JsonReader reader)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        return reader.GetDateTimeOffset();
    }
    public override DateTimeOffset? Read(XmlReader reader)
    {
        if (string.IsNullOrEmpty(reader.Value))
        {
            return null;
        }
        return reader.ReadContentAsDateTimeOffset();
    }
    public override void Write(Utf8JsonWriter writer, DateTimeOffset? value)
    {
        if (value.HasValue)
        {
            writer.WriteStringValue(value.Value);
        }
        writer.WriteNullValue();
    }
    public override void Write(XmlWriter writer, DateTimeOffset? value)
    {
        if (value.HasValue)
        {
            writer.WriteValue(value.Value);
        }
    }
}
