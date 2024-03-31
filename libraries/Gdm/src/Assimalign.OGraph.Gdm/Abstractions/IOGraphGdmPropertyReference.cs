namespace Assimalign.OGraph.Gdm;

/// <summary>
/// A reference to a Complex or Entity Type property.
/// </summary>
public interface IOGraphGdmPropertyReference
{
    /// <summary>
    /// Gets the property being referenced.
    /// </summary>
    IOGraphGdmProperty Definition { get; }
}
