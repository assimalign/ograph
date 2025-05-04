using System;

namespace Assimalign.OGraph.Gdm
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    public sealed class GdmScalarTypeAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type"></param>
        public GdmScalarTypeAttribute(ScalarUnderlyingType type)
        {
            Type = type;
        }

        /// <summary>
        /// The underlying runtime type to use for the entity key.
        /// </summary>
        public ScalarUnderlyingType Type { get; }

        /// <summary>
        /// Write a partial bool method that is called on instantiation.
        /// </summary>
        public bool IncludeIsValidMethod { get; set; }

        /// <summary>
        /// Specifies whether to include an implicit operators that convert to and from 
        /// the underlying runtime type.
        /// </summary>
        public bool IncludeImplicitOperators { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? GdmTypeName { get; set; }

        /// <summary>
        /// The namespace to write the GDM type to.
        /// </summary>
        public string? GdmTypeNamespace { get; set; }
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
}

