using System;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;

public class ComplexType : IOGraphComplexType
{
    public ComplexType()
    {
        this.Properties = new OGraphPropertyCollection();
    }

    public Name Name { get; init; }
    public TypeKind Kind => TypeKind.Complex;
    public IOGraphPropertyCollection Properties { get; }
    public Type? RuntimeType { get; init; }
    public bool IsNullable => true;
    public virtual bool IsAssignable(IOGraphType type)
    {
        return RuntimeType!.IsAssignableFrom(type.RuntimeType);
    }
}
