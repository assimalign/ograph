namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Represents the base element for all graph data model elements.
/// </summary>
public interface IOGraphGdmElement
{
    /// <summary>
    /// Gets the GDM element type.
    /// </summary>
    GdmElementKind ElementKind { get; }

    /// <summary>
    /// Element Metadata.
    /// </summary>
    IOGraphGdmMetaCollection Meta { get; }
}