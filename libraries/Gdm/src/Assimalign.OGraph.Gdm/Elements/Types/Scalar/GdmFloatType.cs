using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmFloatType : GdmScalarType<Single>
{
    public override float Read(ref Utf8JsonReader reader)
    {
        return reader.GetSingle();
    }
    public override float Read(XmlReader reader)
    {
        return reader.ReadContentAsFloat();
    }
    public override void Write(Utf8JsonWriter writer, float value)
    {
        writer.WriteNumberValue(value);
    }
    public override void Write(XmlWriter writer, float value)
    {
        writer.WriteValue(value);
    }
}
