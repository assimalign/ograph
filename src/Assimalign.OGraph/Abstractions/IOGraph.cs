namespace Assimalign.OGraph;

/// <summary>
/// Represents a single graph Model.
/// </summary>
public interface IOGraph
{
    /// <summary>
    /// The name of the graph model.
    /// </summary>
    Name Label { get; }
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
}