using System;
using System.Text.Json;
using System.Xml;

namespace Assimalign.OGraph;

public sealed class StringType : IOGraphType
{
    public Name TypeName => typeof(string).Name;
    public OGraphTypeIdentifier TypeIdentifier => OGraphTypeIdentifier.Primitive;
    public Type? RuntimeType => typeof(string);
    public bool IsNullable => true;
   
}
