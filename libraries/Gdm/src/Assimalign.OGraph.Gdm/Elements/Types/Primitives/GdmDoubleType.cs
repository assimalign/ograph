using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmDoubleType : GdmPrimitiveType<Double>
{
    public override double Read(ref Utf8JsonReader reader)
    {
        return reader.GetDouble();
    }
    public override double Read(XmlReader reader)
    {
        return reader.ReadContentAsDouble();
    }
    public override void Write(Utf8JsonWriter writer, double value)
    {
        writer.WriteNumberValue(value);
    }
    public override void Write(XmlWriter writer, double value)
    {
        writer.WriteValue(value);
    }
}