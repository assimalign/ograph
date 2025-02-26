namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Represents an entire graph model.
/// </summary>
public interface IOGraphGdm : IOGraphGdmLabeledElement
{
    /// <summary>
    /// Get the collection of elements in the model.
    /// </summary>
    IOGraphGdmGraphCollection Graphs { get; }
}