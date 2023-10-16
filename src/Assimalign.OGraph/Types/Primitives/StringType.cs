using System;
using System.Text.Json;
using System.Xml;

namespace Assimalign.OGraph;

public sealed class StringType : IOGraphType
{
    public Name Name => typeof(string).Name;
    public TypeKind Kind => TypeKind.Primitive;
    public Type RuntimeType => typeof(string);
    public bool IsNullable => true;

    public bool IsAssignable(object value)
    {
        return RuntimeType.IsAssignableFrom(value.GetType());
    }

    public bool IsAssignable(IOGraphType type)
    {
        throw new NotImplementedException();
    }
}
