using System;
using System.Xml;
using System.Text.Json;
using System.Globalization;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmUInt64Type : GdmPrimitiveType<UInt64>
{
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
}
