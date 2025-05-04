using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public sealed class GdmDoubleType : GdmScalarType<Double>
{
    public GdmDoubleType() { }
    public GdmDoubleType(GdmGraph graph) : base(graph) { }

    public override GdmPrimitiveType PrimitiveType => GdmPrimitiveType.Float;
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
    public override double Parse(string? value)
    {
        throw new NotImplementedException();
    }
    public override bool TryParse(string? value, out double result)
    {
        throw new NotImplementedException();
    }
}