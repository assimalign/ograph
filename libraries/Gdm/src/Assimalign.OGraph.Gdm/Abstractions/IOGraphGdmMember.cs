namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Represents a member of a <see cref="IOGraphGdmComplexType"/> or <see cref="IOGraphGdmEntityType"/>.
/// </summary>
public interface IOGraphGdmMember : IOGraphGdmBindableElement
{
    /// <summary>
    /// References the entity or complex type in which the property or function is a member of.
    /// </summary>
    IOGraphGdmType DeclaringType { get; }

    /// <summary>
    /// Checks whether the current instance is a function.
    /// </summary>
    /// <param name="function"></param>
    /// <returns></returns>
    bool IsFunction(out IOGraphGdmFunction function);

    /// <summary>
    /// Checks whether the current instance is a property.
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    bool IsProperty(out IOGraphGdmProperty property);
}
