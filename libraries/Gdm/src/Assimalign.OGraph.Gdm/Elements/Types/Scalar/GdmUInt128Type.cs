#if NET8_0_OR_GREATER
using System;
using System.Xml;
using System.Text.Json;
using System.Globalization;

namespace Assimalign.OGraph.Gdm.Elements;

public sealed class GdmUInt128Type : GdmScalarType<UInt128>
{
    public override UInt128 Read(ref Utf8JsonReader reader)
    {
        return UInt128.Parse(reader.ValueSpan,
            NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite,
            NumberFormatInfo.InvariantInfo);
    }
    public override UInt128 Read(XmlReader reader)
    {
        return UInt128.Parse(reader.ReadContentAsString(),
            NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite,
            NumberFormatInfo.InvariantInfo);
    }
    public override void Write(Utf8JsonWriter writer, UInt128 value)
    {
        writer.WriteRawValue(ConvertToBytes(value), true);
    }
    public override void Write(XmlWriter writer, UInt128 value)
    {
        writer.WriteRaw(value.ToString());
    }

    private unsafe byte[] ConvertToBytes(UInt128 value)
    {
        var array = new byte[16];
        fixed (byte* pointer = array)
        {
            *(UInt128*)pointer = value;
        }
        return array;
    }
}
#endif