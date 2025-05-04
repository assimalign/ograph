using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public sealed class GdmTimeType : GdmScalarType<TimeOnly>
{
    public GdmTimeType() { }
    public GdmTimeType(GdmGraph graph) : base(graph) { }
    public override GdmName Name { get; internal set; } = "Time";
    public override GdmPrimitiveType PrimitiveType => GdmPrimitiveType.String;
    public override TimeOnly Read(ref Utf8JsonReader reader)
    {
        return TimeOnly.Parse(reader.GetString()!);
    }
    public override TimeOnly Read(XmlReader reader)
    {
        return TimeOnly.Parse(reader.ReadContentAsString());
    }
    public override void Write(Utf8JsonWriter writer, TimeOnly value)
    {
        writer.WriteStringValue(value.ToString());
    }
    public override void Write(XmlWriter writer, TimeOnly value)
    {
        writer.WriteValue(value.ToString());
    }
    public override TimeOnly Parse(string? value)
    {
        return TimeOnly.Parse(value!);
    }
    public override bool TryParse(string? value, out TimeOnly result)
    {
        return TimeOnly.TryParse(value!, out result);
    }
}