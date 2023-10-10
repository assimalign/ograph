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
    public TypeIdentifier Identifier => TypeIdentifier.Collection;
    public Type RuntimeType => typeof(IEnumerable<>).MakeGenericType(ItemType.RuntimeType!);
    public bool IsNullable => true;
}