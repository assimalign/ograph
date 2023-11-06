using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmNullUInt64Type : GdmPrimitiveType<UInt64?>
{
    public override UInt64? Read(ref Utf8JsonReader reader)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        return reader.GetUInt64();
    }
    public override UInt64? Read(XmlReader reader)
    {
        if (string.IsNullOrEmpty(reader.Value))
        {
            return null;
        }
        var content = reader.ReadContentAsString();
        
        if (uint.TryParse(content, out var value))
        {
            return value;
        }

        throw new Exception("Invalid Content Exception");
    }
    public override void Write(Utf8JsonWriter writer, UInt64? value)
    {
        if (value is null)
        {
            writer.WriteNullValue();
        }
        else
        {
            writer.WriteNumberValue(value.Value);
        }
    }
    public override void Write(XmlWriter writer, UInt64? value)
    {
        if (value is not null)
        {
            //writer.WriteValue(value.Value);
        }
    }
}

