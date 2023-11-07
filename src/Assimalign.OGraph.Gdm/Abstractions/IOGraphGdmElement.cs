namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmElement
{
    /// <summary>
    /// The property name.
    /// </summary>
    Label Label { get; }
    /// <summary>
    /// Gets the GDM element type.
    /// </summary>
    GdmElementType ElementType { get; }
}