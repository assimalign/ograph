using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmNullBooleanType : GdmPrimitiveType<bool?>
{
    public override bool? Read(ref Utf8JsonReader reader)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        return reader.GetBoolean();
    }
    public override bool? Read(XmlReader reader)
    {
        if (string.IsNullOrEmpty(reader.Value))
        {
            return null;
        }
        return reader.ReadContentAsBoolean();
    }
    public override void Write(Utf8JsonWriter writer, bool? value)
    {
        if (value is null)
        {
            writer.WriteNullValue();
        }
        else
        {
            writer.WriteBooleanValue(value.Value);
        }
    }
    public override void Write(XmlWriter writer, bool? value)
    {
        if (value is not null)
        {
            writer.WriteValue(value.Value);
        }
    }
}