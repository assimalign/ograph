using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;

public class ComplexType : IOGraphComplexType
{
    public ComplexType()
    {
        this.Properties = new OGraphPropertyCollection();
    }
    public Name TypeName { get; init; }
    public OGraphTypeIdentifier TypeIdentifier => OGraphTypeIdentifier.Complex;
    public IOGraphPropertyCollection Properties { get; }
    public Type? RuntimeType { get; init; }
    public bool IsNullable => true;

    object IOGraphType.Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    object IOGraphType.Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    void IOGraphType.Write(XmlWriter writer, object value)
    {
        throw new NotImplementedException();
    }

    void IOGraphType.Write(Utf8JsonWriter writer, object value)
    {
        throw new NotImplementedException();
    }
}
