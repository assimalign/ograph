using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

public abstract class GdmPrimitiveType<T> : IOGraphGdmPrimitiveType
{
    public GdmPrimitiveType()
    {
        Label = GetLabel();
    }

    private Label GetLabel()
    {
        var type = typeof(T);
        var typeArgs = type.GetGenericArguments();
        if (typeArgs.Length == 1)
        {
            var typeArg = typeArgs[0];
            if (typeArg.IsValueType)
            {
                var typeAsNull = typeof(Nullable<>).MakeGenericType(typeArg);
                if (typeAsNull.IsAssignableTo(type))
                {
                    return new Label(typeArg.Name).ToPascalCase();
                }
            }
        }
        return new Label(type.Name).ToPascalCase();
    }

    public string[]? Formats { get; }
    public virtual Label Label { get; }
    public GdmTypeKind Kind => GdmTypeKind.Primitive;
    public virtual Type RuntimeType => typeof(T);
    public virtual bool IsAssignableTo(IOGraphGdmType type)
    {
        return RuntimeType!.IsAssignableTo(type.RuntimeType);
    }

    public virtual T Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }
    public virtual T Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    public virtual void Write(Utf8JsonWriter writer, T value)
    {
        throw new NotImplementedException();
    }
    public virtual void Write(XmlWriter writer, T value)
    {
        throw new NotImplementedException();
    }

    object IOGraphGdmType.Read(ref Utf8JsonReader reader) => Read(ref reader);
    object IOGraphGdmType.Read(XmlReader reader) => Read(reader);
    void IOGraphGdmType.Write(Utf8JsonWriter writer, object value) => Write(writer, AssertType(value));
    void IOGraphGdmType.Write(XmlWriter writer, object value) => Write(writer, AssertType(value));

    private T AssertType(object value)
    {
        if (value is not T type)
        {
            throw new InvalidOperationException($"Could not write type {value.GetType().Name}. Expected {typeof(T).Name}");
        }
        return type;
    }
}
