using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmDateTimeType : GdmPrimitiveType<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader)
    {
        return reader.GetDateTime();
    }
    public override DateTime Read(XmlReader reader)
    {
        return reader.ReadContentAsDateTime();
    }
    public override void Write(Utf8JsonWriter writer, DateTime value)
    {
        writer.WriteStringValue(value);
    }
    public override void Write(XmlWriter writer, DateTime value)
    {
        writer.WriteValue(value);
    }
}
