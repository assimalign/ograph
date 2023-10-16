using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

public class CollectionType<TType> : IOGraphCollectionType
    where TType : IOGraphType, new()
{
    public CollectionType()
    {
        ItemType = new TType();
    }
    public virtual Name Name
    {
        get
        {
            return $"{ItemType.Name}Collection";
        }
    }

    public TType ItemType { get; }
    IOGraphType IOGraphCollectionType.ItemType => ItemType;
    public TypeKind Kind => TypeKind.Collection;
    public Type RuntimeType => typeof(IEnumerable<>).MakeGenericType(ItemType.RuntimeType!);
    public bool IsNullable => true;
    public virtual bool IsAssignable(IOGraphType type)
    {
        return RuntimeType!.IsAssignableFrom(type.RuntimeType);
    }
}