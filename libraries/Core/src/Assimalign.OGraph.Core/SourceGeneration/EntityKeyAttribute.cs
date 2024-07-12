using System;

namespace Assimalign.OGraph;


[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class EntityKeyAttribute : Attribute
{
    public EntityKeyAttribute(EntityKeyType keyType)
    {
        KeyType = keyType;
    }

    public EntityKeyType KeyType { get; }
}
