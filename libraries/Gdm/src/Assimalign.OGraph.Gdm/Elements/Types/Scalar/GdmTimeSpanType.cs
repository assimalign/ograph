using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public sealed class GdmTimeSpanType : GdmScalarType<TimeSpan>
{
    public GdmTimeSpanType(GdmGraph graph)
    {
        Graph = ThrowHelper.ThrowIfNull(graph, nameof(graph));
    }

    public override Label Label => "TimeSpan";
    public override GdmGraph Graph { get; internal set; }
    public override GdmPrimitiveType PrimitiveType => GdmPrimitiveType.String;
    public override TimeSpan Read(ref Utf8JsonReader reader)
    {
        return TimeSpan.Parse(reader.GetString()!);
    }
    public override TimeSpan Read(XmlReader reader)
    {
        return TimeSpan.Parse(reader.ReadContentAsString());
    }
    public override void Write(Utf8JsonWriter writer, TimeSpan value)
    {
        writer.WriteStringValue(value.ToString());
    }
    public override void Write(XmlWriter writer, TimeSpan value)
    {
        writer.WriteValue(value.ToString());
    }
    public override TimeSpan Parse(string? value)
    {
        return TimeSpan.Parse(value!);
    }
    public override bool TryParse(string? value, out TimeSpan result)
    {
        return TimeSpan.TryParse(value!, out result);
    }
}
