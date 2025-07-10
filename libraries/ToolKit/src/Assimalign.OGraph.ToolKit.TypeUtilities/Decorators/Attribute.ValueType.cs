using System;
using System.Collections.Generic;
using System.Text;

namespace System;

public enum ValueUnderlyingType
{
    Int,
    Short,
    Long,
    UInt,
    UShort,
    ULong,
    String,
    Guid,
    Ulid
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
    public ValueTypeAttribute(ValueUnderlyingType type)
    {
        Type = type;
    }

    /// <summary>
    /// 
    /// </summary>
    public ValueUnderlyingType Type { get; }

    /// <summary>
    /// Write a partial bool method that is called on instantiation.
    /// </summary>
    public bool IncludeIsValidMethod { get; set; }

    /// <summary>
    /// Specifies whether to include an implicit operators that convert to and from 
    /// the underlying runtime type.
    /// </summary>
    public bool IncludeImplicitOperators { get; set; }


}
