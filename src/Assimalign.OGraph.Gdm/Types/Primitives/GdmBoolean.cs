using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmBoolean : GdmPrimitiveType<bool>
{
    public override bool Read(ref Utf8JsonReader reader)
    {
        return reader.GetBoolean();
    }

    public override bool Read(XmlReader reader)
    {
        return reader.ReadElementContentAsBoolean();
    }

    public override void Write(Utf8JsonWriter writer, bool value)
    {
        writer.WriteBooleanValue(value);
    }

    public override void Write(XmlWriter writer, bool value)
    {
        writer.WriteValue(value);
    }
}
