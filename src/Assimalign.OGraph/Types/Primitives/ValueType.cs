using System;
using System.Xml;
using System.Text.Json;

namespace Assimalign.OGraph;

public abstract class ValueType<T> 
    where T : struct
{
    public Name TypeName => nameof(T);
    public OGraphTypeIdentifier TypeIdentifier => OGraphTypeIdentifier.Primitive;
    public Type? RuntimeType => typeof(T);

    public virtual object? Value { get; }
}
