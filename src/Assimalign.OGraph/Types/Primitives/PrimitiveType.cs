using System;

namespace Assimalign.OGraph;

public abstract class PrimitiveType<T> : IOGraphPrimitiveType
    where T : struct
{


    public virtual Name TypeName => typeof(T).Name;
    public OGraphTypeIdentifier TypeIdentifier => OGraphTypeIdentifier.Primitive;
    public virtual Type? RuntimeType => typeof(T);
    public bool IsNullable { get; internal set; }
}
