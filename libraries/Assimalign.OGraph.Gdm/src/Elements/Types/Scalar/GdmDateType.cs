using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public sealed class GdmDateType : GdmValueScalarType<DateOnly>
{
    public GdmDateType(GdmGraph graph) : base("Date", graph) { }
    public override GdmPrimitiveType PrimitiveType { get; } = GdmPrimitiveType.String;
    public override DateOnly Read(ref Utf8JsonReader reader)
    {
        return DateOnly.Parse(reader.GetString()!);
    }
    public override DateOnly Read(XmlReader reader)
    {
        return DateOnly.Parse(reader.ReadContentAsString());
    }
    public override void Write(Utf8JsonWriter writer, DateOnly value)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
    }
    public override void Write(XmlWriter writer, DateOnly value)
    {
        writer.WriteValue(value.ToString("yyyy-MM-dd"));
    }
    public override DateOnly Parse(string? value)
    {
        return DateOnly.Parse(value!);
    }
    public override bool TryParse(string? value, out DateOnly result)
    {
        return DateOnly.TryParse(value!, out result);
    }
}