using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

public abstract class PrimitiveType<T> : IOGraphGdmPrimitiveType
    where T : struct
{
    public virtual Label Label => typeof(T).Name;
    public TypeKind Kind => TypeKind.Primitive;
    public virtual Type RuntimeType => typeof(T);
    public virtual bool IsAssignableTo(IOGraphGdmType type)
    {
        return RuntimeType!.IsAssignableFrom(type.RuntimeType);
    }

    object IOGraphGdmType.Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    object IOGraphGdmType.Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    void IOGraphGdmType.Write(Utf8JsonWriter writer, object value)
    {
        throw new NotImplementedException();
    }

    void IOGraphGdmType.Write(XmlWriter writer, object value)
    {
        throw new NotImplementedException();
    }
}
