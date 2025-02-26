using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

public abstract class GdmType<T> : GdmType
{
    protected GdmType()
    {
        var typeName = RuntimeType.Name;

        // Let's only override the label if it has valid characters
        if (GdmName.IsValid(typeName))
        {
            Name = typeName;
        }
    }

    public override GdmName Name { get; internal set; }
    public override Type RuntimeType { get; internal set; } = typeof(T);
    public abstract new T Read(ref Utf8JsonReader reader);
    public abstract new T Read(XmlReader reader);
    public abstract void Write(Utf8JsonWriter writer, T value);
    public abstract void Write(XmlWriter writer, T value);
}

///// <summary>
///// An abstract base type.
///// </summary>
///// <typeparam name="T"></typeparam>
//[DebuggerDisplay("{Label} [Type]")]
//public abstract class GdmTypeOld<T> : IOGraphGdmType
//{
//    public GdmType()
//    {
//        var typeName = RuntimeType.Name;
        
//        // Let's only override the label if it has valid characters
//        if (Label.IsValid(typeName))
//        {
//            Label = typeName;
//        }
//    }

//    public Type RuntimeType { get; } = typeof(T);
//    public IOGraphGdmGraph Graph { get; internal set; } = default!;
//    public virtual Label Label { get; internal set; }
//    public abstract GdmTypeKind Kind { get; }
//    public IOGraphGdmMetadata Meta { get; } = new GdmMetadata();
//    public GdmElementKind ElementKind { get; } = GdmElementKind.Type;

//    public abstract T Read(ref Utf8JsonReader reader);
//    public abstract T Read(XmlReader reader);
//    public abstract void Write(Utf8JsonWriter writer, T value);
//    public abstract void Write(XmlWriter writer, T value);


//    #region Explicit Interface Implementations

//    object IOGraphGdmType.Read(ref Utf8JsonReader reader) => Read(ref reader)!;
//    object IOGraphGdmType.Read(XmlReader reader) => Read(reader)!;
//    void IOGraphGdmType.Write(Utf8JsonWriter writer, object value) => Write(writer, AssertType(value));
//    void IOGraphGdmType.Write(XmlWriter writer, object value) => Write(writer, AssertType(value));

//    #endregion


//    internal TValue AssertNotNull<TValue>(TValue value, string paramName)
//    {
//        if (value is null)
//        {
//            ThrowHelper.ThrowArgumentNullException(nameof(paramName));
//        }
//        return value;
//    }

//    private T AssertType(object value)
//    {
//        if (value is not T)
//        {
//            ThrowHelper.ThrowInvalidSerializationTypeException(typeof(T), value.GetType());
//        }
//        return (T)value;
//    }
//}