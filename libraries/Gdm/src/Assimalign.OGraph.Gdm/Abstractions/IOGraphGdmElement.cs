namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Represents the base element for all graph data model elements.
/// </summary>
public interface IOGraphGdmElement
{
    /// <summary>
    /// The element label.
    /// </summary>
    Label Label { get; }
    /// <summary>
    /// Gets the GDM element type.
    /// </summary>
    GdmElementKind ElementKind { get; }
}