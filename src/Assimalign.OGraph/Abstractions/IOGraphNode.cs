namespace Assimalign.OGraph;

/// <summary>
/// Represents a single entity and it's structure within the graph Model.
/// </summary>
/// <remarks>
/// A Node is also referred to as a Vertex.
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
    /// A collection of types that make up individual entities.
    /// </summary>
    /// <remarks>
    /// These types can be though of as the available structure 
    /// that can be used to retrieve, create, or modify a node.
    /// </remarks>
    IOGraphType Type { get; }
    /// <summary>
    /// A collection of edges that are connected to this node.
    /// </summary>
    IOGraphEdgeCollection Edges { get; }
}
