using System;
using System.Text.Json;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

public sealed class StringType : IOGraphGdmPrimitiveType
{
    public Label Label => typeof(string).Name;
    public TypeKind Kind => TypeKind.Primitive;
    public Type RuntimeType => typeof(string);
    public bool IsNullable => true;

    public bool IsAssignable(IOGraphGdmType value)
    {
        return RuntimeType.IsAssignableFrom(value.GetType());
    }

    public bool IsAssignableTo(IOGraphGdmType type)
    {
        throw new NotImplementedException();
    }

    public object Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    public object Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public void Write(Utf8JsonWriter writer, object value)
    {
        throw new NotImplementedException();
    }

    public void Write(XmlWriter writer, object value)
    {
        throw new NotImplementedException();
    }
}
