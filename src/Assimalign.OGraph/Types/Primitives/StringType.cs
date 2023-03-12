using System;
using System.Text.Json;
using System.Xml;

namespace Assimalign.OGraph;

public sealed class StringType : IOGraphType
{
    public Name TypeName => "String";
    public OGraphTypeIdentifier TypeIdentifier => throw new System.NotImplementedException();

    public Type? RuntimeType => throw new NotImplementedException();

    public bool IsRuntimeType => throw new NotImplementedException();

    public bool TryReadJson(Utf8JsonReader reader, out object value)
    {
        throw new NotImplementedException();
    }

    public bool TryReadXml(XmlReader reader, out object value)
    {
        throw new NotImplementedException();
    }

    public bool TryWriteJson(Utf8JsonWriter writer, object value)
    {
        throw new NotImplementedException();
    }

    public bool TryWriteXml(XmlWriter writer, object value)
    {
        throw new NotImplementedException();
    }
}
