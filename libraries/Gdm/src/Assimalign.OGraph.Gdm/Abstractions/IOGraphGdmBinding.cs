namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Extends functionality of GDM elements.
/// </summary>
public interface IOGraphGdmBinding
{
    /// <summary>
    /// The binding label.
    /// </summary>
    Label Label { get; }
    /// <summary>
    /// The binding kind.
    /// </summary>
    GdmBindingKind Kind { get; }
    /// <summary>
    /// Binding metadata.
    /// </summary>
    IOGraphGdmMetadata Metadata { get; }
}
