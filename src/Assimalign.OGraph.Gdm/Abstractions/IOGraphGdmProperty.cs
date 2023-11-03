using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmProperty
{
    /// <summary>
    /// The property name.
    /// </summary>
    Label Name { get; }
    /// <summary>
    /// The OGraph Property Type.
    /// </summary>
    IOGraphGdmTypeReference Type { get; }
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
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerable<IOGraphGdmBinding> GetBindings();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="binding"></param>
    /// <returns></returns>
    void AddBinding(IOGraphGdmInputBinding binding);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="binding"></param>
    /// <returns></returns>
    void AddBinding(IOGraphGdmOutputBinding binding);
}