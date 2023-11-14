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
    public virtual Label Label
    {
        get
        {
            return $"{ItemType.Label}Collection";
        }
    }

    public TType ItemType { get; }
    IOGraphType IOGraphCollectionType.ItemType => ItemType;
    public TypeKind Kind => TypeKind.Collection;
    public Type RuntimeType => typeof(IEnumerable<>).MakeGenericType(ItemType.RuntimeType!);
    public virtual bool IsAssignableTo(IOGraphType type)
    {
        return RuntimeType!.IsAssignableFrom(type.RuntimeType);
    }
}