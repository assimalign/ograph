namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Extends functionality of GDM elements.
/// </summary>
public interface IOGraphGdmBinding
{
    /// <summary>
    /// The binding label.
    /// </summary>
    GdmName Name { get; }

    /// <summary>
    /// The element the binding is associated with.
    /// </summary>
    IOGraphGdmBindableElement Element { get; }
}