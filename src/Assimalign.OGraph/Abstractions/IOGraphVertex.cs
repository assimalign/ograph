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
    Label Label { get; }
    /// <summary>
    /// The type bound to this vertex.
    /// </summary>
    IOGraphType Type { get; } // TODO: Revisit whether I should specify IOGraphEntityType
    /// <summary>
    /// A collection of edges that are connected to this node.
    /// </summary>
    IOGraphEdgeCollection Edges { get; }
    /// <summary>
    /// Represents arbitrary metadata that can be associated with this node.
    /// </summary>
    IOGraphMetadata Metadata { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IOGraphPropertyCollection GetProperties();
    /// <summary>
    /// Gets the collection of operations bound to this vertex.
    /// </summary>
    /// <returns></returns>
    IOGraphOperationCollection GetOperations();
}