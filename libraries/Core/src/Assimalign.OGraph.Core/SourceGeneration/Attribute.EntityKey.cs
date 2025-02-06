using System;
using System.Text.Json.Serialization;

namespace Assimalign.OGraph;

/// <summary>
/// This attribute is used for the underlying source generator when creating custom value types 
/// for entity keys.
/// </summary>
/// <remarks>
/// Use this attribute to leverage the code generator build in which generates 
/// custom structs for entity keys.
/// <code>
/// [EntityKey(EntityKeyRuntimeType.Guid)]
/// public partial struct MyEntityId
/// {
///     // Body of struct is generated automatically
/// }
/// </code>
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class EntityKeyAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="runtimeType"></param>
    public EntityKeyAttribute(EntityKeyRuntimeType runtimeType = EntityKeyRuntimeType.Int)
    {
        RuntimeType = runtimeType;
    }

    /// <summary>
    /// The underlying runtime type to use for the entity key.
    /// </summary>
    public EntityKeyRuntimeType RuntimeType { get; }

    /// <summary>
    /// Specifies whether to include an implicit operators that convert to and from 
    /// the underlying runtime type.
    /// </summary>
    public bool IncludeImplicitOperators { get; set; }

    /// <summary>
    /// Write a partial bool method for 
    /// </summary>
    public bool IncludeIsValidMethod { get; set; }
}

/// <summary>
/// 
/// </summary>
public enum EntityKeyRuntimeType
{
    Int,
    Short,
    Long,
    UInt,
    UShort,
    ULong,
    String,
    Guid
}
