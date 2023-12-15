using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmInt32Type : GdmPrimitiveType<Int32>
{
    public override int Read(ref Utf8JsonReader reader)
    {
        return reader.GetInt32();
    }
    public override int Read(XmlReader reader)
    {
        return reader.ReadContentAsInt();
    }
    public override void Write(Utf8JsonWriter writer, int value)
    {
        writer.WriteNumberValue(value);
    }
    public override void Write(XmlWriter writer, int value)
    {
        writer.WriteValue(value);
    }
}
