using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public sealed class GdmFloatType : GdmScalarType<Single>
{
    public GdmFloatType() { }
    public GdmFloatType(GdmGraph graph) : base(graph) { }

    public override GdmPrimitiveType PrimitiveType => GdmPrimitiveType.Float;
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
    public override float Parse(string? value)
    {
        return float.Parse(value!);
    }
    public override bool TryParse(string? value, out float result)
    {
        return float.TryParse(value!, out result);
    }
}
