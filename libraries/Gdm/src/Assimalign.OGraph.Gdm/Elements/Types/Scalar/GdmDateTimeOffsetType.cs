using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public sealed class GdmDateTimeOffsetType : GdmScalarType<DateTimeOffset>
{
    public GdmDateTimeOffsetType() { }
    public GdmDateTimeOffsetType(GdmGraph graph) : base(graph) { }

    public override GdmPrimitiveType PrimitiveType { get; } = GdmPrimitiveType.String;
    public override DateTimeOffset Read(ref Utf8JsonReader reader)
    {
        return reader.GetDateTimeOffset();
    }
    public override DateTimeOffset Read(XmlReader reader)
    {
        return reader.ReadContentAsDateTimeOffset();
    }
    public override void Write(Utf8JsonWriter writer, DateTimeOffset value)
    {
        writer.WriteStringValue(value);
    }
    public override void Write(XmlWriter writer, DateTimeOffset value)
    {
        writer.WriteValue(value);
    }
    public override DateTimeOffset Parse(string? value)
    {
        return DateTimeOffset.Parse(value!);
    }
    public override bool TryParse(string? value, out DateTimeOffset result)
    {
        return DateTimeOffset.TryParse(value, out result);
    }
}