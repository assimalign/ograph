using System;
using System.Text.Json;
using System.Xml;

namespace Assimalign.OGraph;

public sealed class StringType : IOGraphType
{
    public Name Name => typeof(string).Name;
    public TypeIdentifier Identifier => TypeIdentifier.Primitive;
    public Type? RuntimeType => typeof(string);
    public bool IsNullable => true;
   
}
