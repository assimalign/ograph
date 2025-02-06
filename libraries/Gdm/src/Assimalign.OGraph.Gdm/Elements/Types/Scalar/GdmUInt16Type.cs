using System;
using System.Xml;
using System.Text.Json;
using System.Globalization;

namespace Assimalign.OGraph.Gdm.Elements;

public sealed class GdmUInt16Type : GdmScalarType<UInt16>
{
    public override ushort Read(ref Utf8JsonReader reader)
    {
        return reader.GetUInt16();
    }
    public override ushort Read(XmlReader reader)
    {
        return ushort.Parse(
            reader.ReadContentAsString(),
            NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite,
            NumberFormatInfo.InvariantInfo);
    }
    public override void Write(Utf8JsonWriter writer, ushort value)
    {
        writer.WriteNumberValue(value);
    }
    public override void Write(XmlWriter writer, ushort value)
    {
        writer.WriteValue(value.ToString());
    }
}
