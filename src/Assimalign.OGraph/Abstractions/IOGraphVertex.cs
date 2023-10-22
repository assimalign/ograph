using System;
using System.Linq.Expressions;

namespace Assimalign.OGraph;

/// <summary>
/// Represents a single entity and it's structure within the graph Model.
/// </summary>
/// <remarks>
/// A Node is also referred to as a Vertex.
/// </remarks>
public interface IOGraphVertex
{
    /// <summary>
    /// Represents the label each node should contain.
    /// </summary>
    Name[] Labels { get; }
    /// <summary>
    /// Represents arbitrary metadata that can be associated with this node.
    /// </summary>
    IOGraphMetadata Metadata { get; }
    /// <summary>
    /// The type being bound to this node.
    /// </summary>
    IOGraphType Type { get; }
    /// <summary>
    /// A collection of edges that are connected to this node.
    /// </summary>
    IOGraphEdgeCollection Edges { get; }
    /// <summary>
    /// A collection of operations bound to this node.
    /// </summary>
    IOGraphOperationCollection Operations { get; }
}