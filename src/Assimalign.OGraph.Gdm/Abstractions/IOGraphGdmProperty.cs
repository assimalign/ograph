using System.Threading;
using System.Threading.Tasks;

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
    /// 
    /// </summary>
    IOGraphGdmPropertyGetter Getter { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphGdmPropertySetter Setter { get; }
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
}