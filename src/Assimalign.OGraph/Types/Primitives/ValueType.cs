using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph;

public abstract class ValueType<T> : IOGraphPrimitiveType
    where T : struct
{
    public Name TypeName => nameof(T);
    public OGraphTypeIdentifier TypeIdentifier => OGraphTypeIdentifier.Primitive;
    public Type? RuntimeType => typeof(T);
    public virtual bool IsRuntimeType { get; }

    public abstract bool TryReadJson(Utf8JsonReader reader, out OGraphValue value);
    public abstract bool TryWriteJson(Utf8JsonWriter writer, OGraphValue value);
    public abstract bool TryReadXml(XmlReader reader, out OGraphValue value);
    public abstract bool TryWriteXml(XmlWriter writer, OGraphValue value);
}
