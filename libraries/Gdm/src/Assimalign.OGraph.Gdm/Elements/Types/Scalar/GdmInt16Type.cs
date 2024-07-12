using System;
using System.Xml;
using System.Text.Json;
using System.Globalization;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmInt16Type : GdmScalarType<Int16>
{
    public override short Read(ref Utf8JsonReader reader)
    {
        return reader.GetInt16();
    }
    public override short Read(XmlReader reader)
    {
        return short.Parse(
            reader.ReadContentAsString(),
            NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite,
            NumberFormatInfo.InvariantInfo);
    }
    public override void Write(Utf8JsonWriter writer, short value)
    {
        writer.WriteNumberValue(value);
    }

    public override void Write(XmlWriter writer, short value)
    {
        writer.WriteValue(value);
    }
}
