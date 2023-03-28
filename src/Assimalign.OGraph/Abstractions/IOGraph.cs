namespace Assimalign.OGraph;


/// <summary>
/// Represents a single graph Model.
/// </summary>
public interface IOGraph
{
    /// <summary>
    /// The name of the graph model.
    /// </summary>
    Name Name { get; }
    /// <summary>
    /// A collection of node definitions within the OGraph Model.
    /// </summary>
    IOGraphNodeCollection Nodes { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphEdgeCollection Edges { get; }
    /// <summary>
    /// 
    /// </summary>
    //IOGraphEventCollection Events { get; }
    /// <summary>
    /// Represents a collection of HTTP Operations
    /// </summary>
    IOGraphOperationCollection Operations { get; }
    
}