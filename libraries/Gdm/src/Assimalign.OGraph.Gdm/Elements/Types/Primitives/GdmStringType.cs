using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmStringType : GdmPrimitiveType<string>
{
    public override string Read(ref Utf8JsonReader reader)
    {
        return reader.GetString()!;
    }
    public override string Read(XmlReader reader)
    {
        return reader.ReadContentAsString();
    }
    public override void Write(Utf8JsonWriter writer, string value)
    {
        writer.WriteStringValue(value);
    }
    public override void Write(XmlWriter writer, string value)
    {
        writer.WriteValue(value);
    }
}
