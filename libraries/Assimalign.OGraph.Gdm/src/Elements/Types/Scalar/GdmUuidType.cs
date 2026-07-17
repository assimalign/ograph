using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public sealed class GdmUuidType : GdmValueScalarType<Guid>
{
    public GdmUuidType(GdmGraph graph) : base(graph) { }

    public override GdmPrimitiveType PrimitiveType { get; } = GdmPrimitiveType.String;
    public override string[] Formats => Regex;
    public override Guid Read(ref Utf8JsonReader reader)
    {
        return reader.GetGuid();
    }
    public override Guid Read(XmlReader reader)
    {
        return Guid.Parse(reader.ReadContentAsString());
    }
    public override void Write(Utf8JsonWriter writer, Guid value)
    {
        writer.WriteStringValue(value);
    }
    public override void Write(XmlWriter writer, Guid value)
    {
        writer.WriteValue(value.ToString());
    }
    public override Guid Parse(string? value)
    {
        return Guid.Parse(value!);
    }
    public override bool TryParse(string? value, out Guid result)
    {
        return Guid.TryParse(value!, out result);
    }

    public static string[] Regex => ["^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[1-5][0-9a-fA-F]{3}-[89abAB][0-9a-fA-F]{3}-[0-9a-fA-F]{12}$"];
}
