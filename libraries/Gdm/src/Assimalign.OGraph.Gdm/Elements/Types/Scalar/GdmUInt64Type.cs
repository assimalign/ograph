using System;
using System.Xml;
using System.Text.Json;
using System.Globalization;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public sealed class GdmUInt64Type : GdmScalarType<UInt64>
{
    public GdmUInt64Type() { }
    public GdmUInt64Type(GdmGraph graph) : base(graph) { }


    public override GdmPrimitiveType PrimitiveType => GdmPrimitiveType.Int;
    public override ulong Read(ref Utf8JsonReader reader)
    {
        return reader.GetUInt64();
    }
    public override ulong Read(XmlReader reader)
    {
        return ulong.Parse(
            reader.ReadContentAsString(), 
            NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, 
            NumberFormatInfo.InvariantInfo);
    }
    public override void Write(Utf8JsonWriter writer, ulong value)
    {
        writer.WriteNumberValue(value);
    }
    public override void Write(XmlWriter writer, ulong value)
    {
        writer.WriteValue(value.ToString());
    }
    public override ulong Parse(string? value)
    {
        return ulong.Parse(value!);
    }
    public override bool TryParse(string? value, out ulong result)
    {
        return ulong.TryParse(value!, out result);
    }
}
