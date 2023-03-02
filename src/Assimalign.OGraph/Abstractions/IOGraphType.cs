namespace Assimalign.OGraph;

/// <summary>
/// Types represent primitive, complex, or collection structure that can be 
/// used to define a property, inputs, and outputs of operations within the graph.
/// </summary>
public interface IOGraphType
{
    /// <summary>
    /// The name of the type.
    /// </summary>
    Name TypeName { get; }
    /// <summary>
    /// 
    /// </summary>
    OGraphTypeIdentifier TypeIdentifier { get; }
}