using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmGuidType : GdmPrimitiveType<Guid>
{
    public override Guid Read(ref Utf8JsonReader reader)
    {
        return reader.GetGuid();
    }
    public override Guid Read(XmlReader reader)
    {
        return Guid.Parse(reader.ReadContentAsString());
    }
    public override void Write(Utf8JsonWriter writer, Guid value)
    {
        writer.WriteStringValue(value);
    }
    public override void Write(XmlWriter writer, Guid value)
    {
        writer.WriteValue(value.ToString());
    }
}
