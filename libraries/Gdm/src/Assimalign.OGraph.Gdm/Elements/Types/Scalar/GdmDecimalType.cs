using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

public sealed class GdmDecimalType : GdmScalarType<Decimal>
{
    public override decimal Read(ref Utf8JsonReader reader)
    {
        return reader.GetDecimal();
    }
    public override decimal Read(XmlReader reader)
    {
        return reader.ReadContentAsDecimal();
    }
    public override void Write(Utf8JsonWriter writer, decimal value)
    {
        writer.WriteNumberValue(value);
    }
    public override void Write(XmlWriter writer, decimal value)
    {
        writer.WriteValue(value);
    }
}
