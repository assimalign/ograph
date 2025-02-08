using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public sealed class GdmInt32Type : GdmScalarType<Int32>
{
    public GdmInt32Type(GdmGraph graph)
    {
        Graph = ThrowHelper.ThrowIfNull(graph, nameof(graph));
    }
    public override GdmGraph Graph { get; internal set; }
    public override GdmPrimitiveType PrimitiveType => GdmPrimitiveType.Int;
    public override int Read(ref Utf8JsonReader reader)
    {
        return reader.GetInt32();
    }
    public override int Read(XmlReader reader)
    {
        return reader.ReadContentAsInt();
    }
    public override void Write(Utf8JsonWriter writer, int value)
    {
        writer.WriteNumberValue(value);
    }
    public override void Write(XmlWriter writer, int value)
    {
        writer.WriteValue(value);
    }
    public override int Parse(string? value)
    {
        return int.Parse(value!);
    }
    public override bool TryParse(string? value, out int result)
    {
        return int.TryParse(value!, out result);
    }
}
