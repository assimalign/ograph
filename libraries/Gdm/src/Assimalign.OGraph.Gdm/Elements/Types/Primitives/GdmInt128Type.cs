#if NET7_0_OR_GREATER
using System;
using System.Text.Json;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmInt128Type : GdmPrimitiveType<Int128>
{
    public override Int128 Read(ref Utf8JsonReader reader)
    {
        var span = reader.ValueSpan;
     
        return base.Read(ref reader);
    }

    public override Int128 Read(XmlReader reader)
    {
        return base.Read(reader);
    }

    public override void Write(Utf8JsonWriter writer, Int128 value)
    {
        base.Write(writer, value);
    }

    public override void Write(XmlWriter writer, Int128 value)
    {
        base.Write(writer, value);
    }
}
#endif
