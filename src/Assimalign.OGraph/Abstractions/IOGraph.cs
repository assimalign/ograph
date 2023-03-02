namespace Assimalign.OGraph;


/// <summary>
/// Represents a single Graph Model.
/// </summary>
public interface IOGraph
{
    /// <summary>
    /// The name of the graph model.
    /// </summary>
    Name Name { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphNodeCollection Nodes { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphEventCollection Events { get; }
    /// <summary>
    /// Represents a collection of HTTP Operations
    /// </summary>
    IOGraphOperationCollection Operations { get; }
    
}