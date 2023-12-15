#if NET8_0_OR_GREATER
using System;
using System.Xml;
using System.Text.Json;
using System.Globalization;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmInt128Type : GdmPrimitiveType<Int128>
{
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
