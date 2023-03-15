namespace Assimalign.OGraph;

/// <summary>
/// Represents a single entity and it's structure within the Graph Model.
/// </summary>
/// <remarks>
/// A Root is also referred to as a Vertex.
/// </remarks>
public interface IOGraphNode
{
    /// <summary>
    /// Represents the label each node should contain.
    /// </summary>
    Label Label { get; }
    
    /// <summary>
    /// Represents arbitrary metadata that can be associated with this node.
    /// </summary>
    IOGraphMetadata Metadata { get; }    
    /// <summary>
    /// A collection of edges that are connected to this node.
    /// </summary>
    IOGraphEdgeCollection Edges { get; }
}
