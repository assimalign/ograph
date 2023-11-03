namespace Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
public abstract class IdentifierNode : QueryNode
{
    /// <summary>
    /// The identifier name.
    /// </summary>
    public string? Name { get; init; }
}
