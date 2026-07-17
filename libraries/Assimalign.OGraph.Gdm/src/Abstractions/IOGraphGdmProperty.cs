namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmProperty : IOGraphGdmMember
{
    /// <summary>
    /// The OGraph Property Type.
    /// </summary>
    IOGraphGdmType Type { get; }

    /// <summary>
    /// The getter of the property value.
    /// </summary>
    GdmPropertyGetter Getter { get; }

    /// <summary>
    /// The setter of the property value.
    /// </summary>
    GdmPropertySetter Setter { get; }

    /// <summary>
    /// 
    /// </summary>
    bool IsReadOnly { get; }

    /// <summary>
    /// 
    /// </summary>
    bool IsNullable { get; }
}