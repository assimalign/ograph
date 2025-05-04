using System;
using System.Xml;
using System.Text.Json;
using Assimalign.OGraph.Gdm.Internal;
using System.Runtime.Serialization;

namespace Assimalign.OGraph.Gdm.Elements;

public sealed class GdmDateTimeType : GdmScalarType<DateTime>
{
    public GdmDateTimeType() { }
    public GdmDateTimeType(GdmGraph graph) : base(graph)
    {
       
    }
    //public override string[]? Formats => new[]
    //{
    //    "yyyy-MM-dd",
    //    "yyyyMMdd",
    //    "yyyy-MM-ddTHH:mm:ss",
    //    "yyyy-MM-ddTHH:mm:ss.fffffffK",
    //    "yyyy-MM-ddTHH:mm:ss.fffffffZ",
    //    "yyyy-MM-ddTHH:mm:ss.fffffffzzz"
    //};

    public override GdmPrimitiveType PrimitiveType { get; } = GdmPrimitiveType.String;

    public override DateTime Read(ref Utf8JsonReader reader)
    {
        return reader.GetDateTime();
    }
    public override DateTime Read(XmlReader reader)
    {
        return reader.ReadContentAsDateTime();
    }
    public override void Write(Utf8JsonWriter writer, DateTime value)
    {
        writer.WriteStringValue(value);
    }
    public override void Write(XmlWriter writer, DateTime value)
    {
        writer.WriteValue(value);
    }
    public override DateTime Parse(string? value)
    {
        throw new NotImplementedException();
    }
    public override bool TryParse(string? value, out DateTime result)
    {
        throw new NotImplementedException();
    }
}
