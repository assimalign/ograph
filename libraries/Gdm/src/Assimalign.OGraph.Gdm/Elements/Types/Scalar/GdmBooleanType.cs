using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public class GdmBooleanType : GdmScalarType<bool>
{
    public GdmBooleanType(GdmGraph graph) 
        : base(graph) { }

    public override GdmPrimitiveType PrimitiveType => GdmPrimitiveType.Boolean;
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
    public override bool Parse(string? value)
    {
        return bool.Parse(value!);
    }
    public override bool TryParse(string? value, out bool result)
    {
        return bool.TryParse(value!, out result);
    }
}