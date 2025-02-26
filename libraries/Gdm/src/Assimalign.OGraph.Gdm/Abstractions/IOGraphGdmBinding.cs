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
    /// The binding kind.
    /// </summary>
    GdmBindingKind Kind { get; }
}