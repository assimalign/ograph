using System;
using System.Xml;
using System.Text.Json;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm;

[DebuggerDisplay("Gdm Type ({Kind}): {Label}")]
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
                    return new Label(typeArg.Name).ToCamalCase();
                }
            }
        }
        return new Label(type.Name).ToCamalCase();
    }

    public string[]? Formats { get; }
    public virtual Label Label { get; }
    public GdmTypeKind Kind => GdmTypeKind.Primitive;
    public virtual Type RuntimeType => typeof(T);
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

    object IOGraphGdmType.Read(ref Utf8JsonReader reader) => Read(ref reader)!;
    object IOGraphGdmType.Read(XmlReader reader) => Read(reader)!;
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