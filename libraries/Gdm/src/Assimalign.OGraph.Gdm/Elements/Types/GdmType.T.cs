using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class GdmType<T> : GdmType
{
    #region Constructors

    public GdmType() { }

    public GdmType(GdmGraph graph) : base(graph)
    {
        var typeName = RuntimeType.Name;

        // Let's only override the label if it has valid characters
        if (GdmName.IsValid(typeName))
        {
            Name = typeName;
        }
    }

    #endregion

    public override GdmName Name { get; }
    public override Type RuntimeType { get; } = typeof(T);
    public abstract new T Read(ref Utf8JsonReader reader);
    public abstract new T Read(XmlReader reader);
    public abstract void Write(Utf8JsonWriter writer, T value);
    public abstract void Write(XmlWriter writer, T value);
    public sealed override void Write(Utf8JsonWriter writer, object value)
    {
        Write(writer, ThrowHelper.ThrowIfNotType<T>(value));
    }
    public sealed override void Write(XmlWriter writer, object value)
    {
        Write(writer, ThrowHelper.ThrowIfNotType<T>(value));
    }
}