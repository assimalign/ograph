using System;

namespace Assimalign.OGraph;

/// <summary>
///
/// </summary>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class EntityKeyAttribute : Attribute
{

    public EntityKeyAttribute(EntityKeyKind kind = EntityKeyKind.Int)
    {
        Kind = kind;
    }

    /// <summary>
    /// 
    /// </summary>
    public EntityKeyKind Kind { get; }

    /// <summary>
    /// 
    /// </summary>
    public bool IncludeImplicitOperators { get; set; }
}
