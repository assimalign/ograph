using System;

namespace Assimalign.OGraph;

public abstract class PrimitiveType<T> : IOGraphPrimitiveType
    where T : struct
{
    public virtual Name Name => typeof(T).Name;
    public TypeIdentifier Identifier => TypeIdentifier.Primitive;
    public virtual Type? RuntimeType => typeof(T);
    public bool IsNullable { get; internal set; }
}
