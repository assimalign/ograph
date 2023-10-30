using System;

namespace Assimalign.OGraph;

public abstract class PrimitiveType<T> : IOGraphPrimitiveType
    where T : struct
{
    public virtual Label Label => typeof(T).Name;
    public TypeKind Kind => TypeKind.Primitive;
    public virtual Type RuntimeType => typeof(T);
    public bool IsNullable { get; internal set; }
    public virtual bool IsAssignable(IOGraphType type)
    {
        return RuntimeType!.IsAssignableFrom(type.RuntimeType);
    }
}
