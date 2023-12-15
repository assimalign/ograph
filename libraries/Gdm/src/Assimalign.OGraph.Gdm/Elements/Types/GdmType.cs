using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class GdmType<T> : IOGraphGdmType
{
    internal Label label;

    public GdmType()
    {
        var typeName = RuntimeType.Name;
        // Let's only override the label if it has valid characters
        if (Label.IsValid(typeName))
        {
            label = typeName;
        }
    }

    public Type RuntimeType => typeof(T);
    public GdmElementType ElementType => GdmElementType.Type;

    public virtual Label Label => label;
    public abstract GdmTypeKind Kind { get; }
    public abstract T Read(ref Utf8JsonReader reader);
    public abstract T Read(XmlReader reader);
    public abstract void Write(Utf8JsonWriter writer, T value);
    public abstract void Write(XmlWriter writer, T value);


    object IOGraphGdmType.Read(ref Utf8JsonReader reader) => Read(ref reader)!;
    object IOGraphGdmType.Read(XmlReader reader) => Read(reader)!;
    void IOGraphGdmType.Write(Utf8JsonWriter writer, object value) => Write(writer, AssertType(value));
    void IOGraphGdmType.Write(XmlWriter writer, object value) => Write(writer, AssertType(value));

    

    private T AssertType(object value)
    {
        if (value is not T)
        {
            GdmThrowHelper.ThrowInvalidTypeSerializationException(typeof(T), value.GetType());
        }
        return (T)value;
    }
}