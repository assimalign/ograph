using System;
using System.Xml;
using System.Text.Json;
using Assimalign.OGraph.Gdm.Internal;

namespace Assimalign.OGraph.Gdm.Elements;

public sealed class GdmBooleanType : GdmScalarType<bool>
{
    public override bool Read(ref Utf8JsonReader reader)
    {
        return reader.GetBoolean();
    }
    public override bool Read(XmlReader reader)
    {
        return reader.ReadContentAsBoolean();
    }
    public override void Write(Utf8JsonWriter writer, bool value)
    {
        writer.WriteBooleanValue(value);
    }
    public override void Write(XmlWriter writer, bool value)
    {
        writer.WriteValue(value);
    }
    public override bool Parse(object? value)
    {
        if (value is not string str)
        {
            throw new ArgumentException("");
        }

        return bool.Parse(str);
    }
    public override bool TryParse(object? value, out bool result)
    {
        result = false;
        if (value is not string str)
        {
            return false;
        }
        return bool.TryParse(str, out result);
    }
}