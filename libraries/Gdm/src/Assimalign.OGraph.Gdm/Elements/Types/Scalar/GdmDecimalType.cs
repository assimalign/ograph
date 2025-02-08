using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public sealed class GdmDecimalType : GdmScalarType<Decimal>
{
    public GdmDecimalType(GdmGraph graph)
    {
        Graph = ThrowHelper.ThrowIfNull(graph, nameof(graph));
    }
    public override GdmGraph Graph { get; internal set; }
    public override GdmPrimitiveType PrimitiveType => GdmPrimitiveType.Float;
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
    public override decimal Parse(string? value)
    {
        return decimal.Parse(value!);
    }
    public override bool TryParse(string? value, out decimal result)
    {
        return decimal.TryParse(value!, out result);
    }
}
