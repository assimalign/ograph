namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Represents an entire graph model.
/// </summary>
public interface IOGraphGdm : IOGraphGdmBindableElement
{
    /// <summary>
    /// Get the collection of elements in the model.
    /// </summary>
    IOGraphGdmElementCollection Elements { get; }
}