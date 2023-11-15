using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmNullDateTimeType : GdmPrimitiveType<DateTime?>
{
    public override DateTime? Read(ref Utf8JsonReader reader)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        return reader.GetDateTime();
    }
    public override DateTime? Read(XmlReader reader)
    {
        if (string.IsNullOrEmpty(reader.Value))
        {
            return null;
        }
        return reader.ReadContentAsDateTime();
    }
    public override void Write(Utf8JsonWriter writer, DateTime? value)
    {
        if (value.HasValue)
        {
            writer.WriteStringValue(value.Value);
        }
        writer.WriteNullValue();
    }
    public override void Write(XmlWriter writer, DateTime? value)
    {
        if (value.HasValue)
        {
            writer.WriteValue(value.Value);
        }
    }
}
