using System;
using System.Xml;
using System.Text.Json;
using System.Globalization;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmUInt32Type : GdmScalarType<UInt32>
{
    public override uint Read(ref Utf8JsonReader reader)
    {
        return reader.GetUInt32();
    }
    public override uint Read(XmlReader reader)
    {
        return uint.Parse(
            reader.ReadContentAsString(),
            NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite,
            NumberFormatInfo.InvariantInfo);
    }
    public override void Write(Utf8JsonWriter writer, uint value)
    {
        writer.WriteNumberValue(value);
    }
    public override void Write(XmlWriter writer, uint value)
    {
        writer.WriteValue(value.ToString());
    }
}
