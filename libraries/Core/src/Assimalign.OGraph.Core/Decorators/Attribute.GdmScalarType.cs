using System;

namespace Assimalign.OGraph;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public sealed class GdmScalarTypeAttribute : Attribute
{
    public GdmScalarTypeAttribute(ScalarUnderlyingType underlyingType)
    {
        UnderlyingType = underlyingType;
    }

    /// <summary>
    /// The underlying runtime type to use for the entity key.
    /// </summary>
    public ScalarUnderlyingType UnderlyingType { get; }

    /// <summary>
    /// Specifies whether to include an implicit operators that convert to and from 
    /// the underlying runtime type.
    /// </summary>
    public bool IncludeImplicitOperators { get; set; }

    /// <summary>
    /// Write a partial bool method for 
    /// </summary>
    public bool IncludeIsValidMethod { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool ExcludeGdmType { get; set; }

    /// <summary>
    /// The namespace to write the GDM type to.
    /// </summary>
    public string GdmTypeNamespace { get; set; }
}

public enum ScalarUnderlyingType
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