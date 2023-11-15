namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Represents a single graph Model.
/// </summary>
public interface IOGraphGdm
{
    /// <summary>
    ///The label of the Graph Model.
    /// </summary>
    /// <remarks>
    /// The label of the Graph model acts as a namespace. In terms a of a domain,
    /// there can be multiple models
    /// </remarks>
    Label Label { get; }
    /// <summary>
    /// Get the collection of elements in the model.
    /// </summary>
    IOGraphGdmElementCollection Elements { get; }
}