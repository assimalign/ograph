using System;

namespace Assimalign.OGraph;


/// <summary>
/// Represents a single entity and it's structure within the Graph Model.
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
    /// A collection of edges that are connected to this node.
    /// </summary>
    IOGraphEdgeCollection Edges { get; set; }
    /// <summary>
    /// The set of key value pairs that are associated with this node.
    /// </summary>
    IOGraphNodePropertyCollection Properties { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    void Configure(IOGraphNodeDescriptor descriptor);
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IOGraphNode<T> : IOGraphNode
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    void Configure(IOGraphNodeDescriptor<T> descriptor);
}
