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

    public EntityKeyAttribute(EntityKeyType keyType, bool al )
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    public EntityKeyType KeyType { get; }

    /// <summary>
    /// 
    /// </summary>
    public bool AllowImplicitOperators { get; set; }
}
