using System;

namespace Assimalign.OGraph;


/// <summary>
///
/// </summary>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class EntityKeyAttribute : Attribute
{
    public EntityKeyAttribute(EntityKeyType keyType)
    {
        KeyType = keyType;
    }
    /// <summary>
    /// 
    /// </summary>
    public EntityKeyType KeyType { get; }
}
