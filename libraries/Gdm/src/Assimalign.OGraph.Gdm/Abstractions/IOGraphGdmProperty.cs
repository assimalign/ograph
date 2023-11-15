using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmProperty : IOGraphGdmBindingElement
{
    /// <summary>
    /// The OGraph Property Type.
    /// </summary>
    IOGraphGdmTypeReference Type { get; }
    /// <summary>
    /// References the entity or complex type in which the property is a member of.
    /// </summary>
    IOGraphGdmTypeReference DeclaringType { get; }
    /// <summary>
    /// Metadata of the property.
    /// </summary>
    IOGraphGdmMetadata Metadata { get; }
    /// <summary>
    /// Specifies whether the property the primary key.
    /// </summary>
    bool IsKey { get; }
    /// <summary>
    /// Computed properties extend complex or entity types that 
    /// are invoked at runtime.
    /// </summary>
    bool IsComputed { get; }
    /// <summary>
    /// 
    /// </summary>
    bool IsNullable { get; } 
    /// <summary>
    /// 
    /// </summary>
    GdmPropertyGetter Getter { get; }
    /// <summary>
    /// 
    /// </summary>
    GdmPropertySetter Setter { get; }
}