namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmProperty : IOGraphGdmLabeledElement
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