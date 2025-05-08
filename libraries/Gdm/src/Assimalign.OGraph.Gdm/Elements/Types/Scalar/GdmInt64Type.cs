using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public sealed class GdmInt64Type : GdmValueScalarType<Int64>
{
    public GdmInt64Type(GdmGraph graph) : base(graph) { }
    public override GdmPrimitiveType PrimitiveType { get; } = GdmPrimitiveType.Int;
    public override long Read(ref Utf8JsonReader reader)
    {
        return reader.GetInt64();
    }
    public override long Read(XmlReader reader)
    {
        return reader.ReadContentAsLong();
    }
    public override void Write(Utf8JsonWriter writer, long value)
    {
        writer.WriteNumberValue(value);
    }
    public override void Write(XmlWriter writer, long value)
    {
        writer.WriteValue(value);
    }
    public override long Parse(string? value)
    {
        return long.Parse(value!);
    }
    public override bool TryParse(string? value, out long result)
    {
        return long.TryParse(value!, out result);
    }
}