using System;
using System.Text.Json;
using System.Xml;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;


/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class GdmScalarType<T> : GdmScalarType
{
    /// <summary>
    /// 
    /// </summary>
    public GdmScalarType() : base()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="graph"></param>
    public GdmScalarType(GdmGraph graph) : base(graph)
    {
    }

    public override GdmName Name { get; } = typeof(T).Name;
    public sealed override Type RuntimeType { get; } = typeof(T);
    public abstract T Parse(string? value);
    public abstract bool TryParse(string? value, out T result);
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
    public sealed override object Parse(object? value)
    {
        return Parse(ThrowHelper.ThrowIfNotType<string>(value))!;
    }
    public sealed override bool TryParse(object? value, out object? result)
    {
        result = default;
        if (value is string str && TryParse(str, out var r))
        {
            result = r;
            return true;
        }
        return false;
    }

    internal override bool IsOfType(Type type)
    {
        return typeof(T).IsAssignableTo(type) || typeof(T?).IsAssignableTo(type);
    }
}

