using System;

namespace Assimalign.OGraph;

/*
    
    Mode -> 
        Operation 1 <--> Type
        Operation 2

 */


/// <summary>
/// 
/// </summary>
/// <remarks>
/// A Node is also referred to as a Vertex.
/// </remarks>
public interface IOGraphNode
{
    /// <summary>
    /// 
    /// </summary>
    bool IsRoot { get; }
    /// <summary>
    /// 
    /// </summary>
    Name Name { get; }
    /// <summary>
    /// The associated type of the node.
    /// </summary>
    /// <remarks>
    /// Nodes tend to be associated to multiple operations.
    /// </remarks>
    IOGraphTypeCollection Types { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphEdgeCollection Edges { get; set; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphOperationCollection Operations { get; }
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
