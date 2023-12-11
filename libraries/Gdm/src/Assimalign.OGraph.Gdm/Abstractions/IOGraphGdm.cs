namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Represents a single graph Model.
/// </summary>
public interface IOGraphGdm : IOGraphGdmBindingElement
{
    /// <summary>
    /// Get the collection of elements in the model.
    /// </summary>
    IOGraphGdmElementCollection Elements { get; }
}