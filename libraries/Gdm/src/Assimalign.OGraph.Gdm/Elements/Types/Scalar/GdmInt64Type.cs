using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

public sealed class GdmInt64Type : GdmScalarType<Int64>
{
    public override long Read(ref Utf8JsonReader reader)
    {
        return reader.GetInt64();
    }
    public override long Read(XmlReader reader)
    {
        return reader.ReadContentAsLong();
    }
    public override void Write(Utf8JsonWriter writer, long value)
    {
        writer.WriteNumberValue(value);
    }
    public override void Write(XmlWriter writer, long value)
    {
        writer.WriteValue(value);
    }
}
