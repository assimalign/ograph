namespace Assimalign.OGraph;

/// <summary>
/// Represents a single graph Model.
/// </summary>
public interface IOGraph
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
    /// 
    /// </summary>
    IOGraphTypeCollection Types { get; }
    /// <summary>
    /// Gets the edge collection.
    /// </summary>
    IOGraphEdgeCollection Edges { get; }
    /// <summary>
    /// A collection of vertex definitions within the OGraph Model.
    /// </summary>
    IOGraphVertexCollection Vertices { get; }
    /// <summary>
    /// Represents a collection of HTTP Operations
    /// </summary>
    IOGraphOperationCollection Operations { get; }
    /// <summary>
    /// Generate an executor from the current graph model.
    /// </summary>
    /// <returns></returns>
    IOGraphExecutor GetExecutor();
}