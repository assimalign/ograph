using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmUriType : GdmScalarType<Uri>
{
    public override Uri Read(ref Utf8JsonReader reader)
    {
        return new Uri(reader.GetString()!);
    }
    public override Uri Read(XmlReader reader)
    {
        return new Uri(reader.ReadContentAsString());
    }
    public override void Write(Utf8JsonWriter writer, Uri value)
    {
        writer.WriteStringValue(value.ToString());
    }
    public override void Write(XmlWriter writer, Uri value)
    {
        writer.WriteValue(value.ToString());
    }
}
