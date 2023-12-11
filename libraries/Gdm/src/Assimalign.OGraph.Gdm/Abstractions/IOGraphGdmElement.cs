namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmElement
{
    /// <summary>
    /// The element label.
    /// </summary>
    Label Label { get; }
    /// <summary>
    /// Gets the GDM element type.
    /// </summary>
    GdmElementType ElementType { get; }
}