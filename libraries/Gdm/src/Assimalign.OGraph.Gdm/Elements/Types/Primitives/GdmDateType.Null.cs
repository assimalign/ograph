using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmNullDateType : GdmPrimitiveType<DateOnly?>
{
    public override Label Label => "Date";
    public override DateOnly? Read(ref Utf8JsonReader reader)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        return DateOnly.Parse(reader.GetString()!);
    }
    public override DateOnly? Read(XmlReader reader)
    {
        return DateOnly.Parse(reader.ReadContentAsString());
    }
    public override void Write(Utf8JsonWriter writer, DateOnly? value)
    {
        if (value.HasValue)
        {
            writer.WriteStringValue(value.Value.ToString("yyyy-MM-dd"));
        }
        writer.WriteNullValue();
    }
    public override void Write(XmlWriter writer, DateOnly? value)
    {
        if (value.HasValue)
        {
            writer.WriteValue(value.Value.ToString("yyyy-MM-dd"));
        }
    }
}
