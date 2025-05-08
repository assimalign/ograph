#if NET8_0_OR_GREATER
using System;
using System.Xml;
using System.Text.Json;
using System.Globalization;
using Assimalign.OGraph.Gdm.Internal;

namespace Assimalign.OGraph.Gdm.Elements;

public sealed class GdmInt128Type : GdmValueScalarType<Int128>
{
    public GdmInt128Type(GdmGraph graph) : base(graph) { }
    public override GdmPrimitiveType PrimitiveType { get; } = GdmPrimitiveType.Int;
    public override Int128 Read(ref Utf8JsonReader reader)
    {
        return Int128.Parse(reader.ValueSpan,
            NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite,
            NumberFormatInfo.InvariantInfo);
    }
    public override Int128 Read(XmlReader reader)
    {
        return Int128.Parse(reader.ReadContentAsString(),
            NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite,
            NumberFormatInfo.InvariantInfo);
    }
    public override void Write(Utf8JsonWriter writer, Int128 value)
    {
        writer.WriteRawValue(ConvertToBytes(value), true);
    }
    public override void Write(XmlWriter writer, Int128 value)
    {
        writer.WriteRaw(value.ToString());
    }
    public override Int128 Parse(string? value)
    {
        return Int128.Parse(value!);
    }
    public override bool TryParse(string? value, out Int128 result)
    {
        return Int128.TryParse(value, out result);
    }
    private unsafe byte[] ConvertToBytes(Int128 value)
    {
        var array = new byte[16];
        fixed (byte* pointer = array)
        {
            *(Int128*)pointer = value;
        }
        return array;
    }
}
#endif
