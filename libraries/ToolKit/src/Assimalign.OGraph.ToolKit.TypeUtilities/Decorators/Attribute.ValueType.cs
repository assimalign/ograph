using System;
using System.Collections.Generic;
using System.Text;

namespace System;

/// <summary>
/// 
/// </summary>
public enum UnderlyingType
{
    /// <summary>
    /// 
    /// </summary>
    Int,
    /// <summary>
    /// 
    /// </summary>
    Short,
    /// <summary>
    /// 
    /// </summary>
    Long,
    /// <summary>
    /// 
    /// </summary>
    UInt,
    /// <summary>
    /// 
    /// </summary>
    UShort,
    /// <summary>
    /// 
    /// </summary>
    ULong,
    /// <summary>
    /// 
    /// </summary>
    Double,
    /// <summary>
    /// 
    /// </summary>
    Decimal,
    /// <summary>
    /// 
    /// </summary>
    String,
    /// <summary>
    /// 
    /// </summary>
    Guid,
}

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public sealed class ValueTypeAttribute : Attribute
{
    /// <summary>
    /// The default constructor of
    /// </summary>
    /// <param name="type"></param>
    public ValueTypeAttribute(UnderlyingType type)
    {
        UnderlyingType = type;
    }

    /// <summary>
    /// The underlying type of the value type.
    /// </summary>
    public UnderlyingType UnderlyingType { get; }

    /// <summary>
    /// Write a partial bool method that is called on instantiation.
    /// </summary>
    public bool IncludeIsValidMethod { get; init; }

    /// <summary>
    /// Specifies whether to include an implicit operators that convert to and from 
    /// the underlying runtime type.
    /// </summary>
    public bool IncludeImplicitOperators { get; init; }
}
