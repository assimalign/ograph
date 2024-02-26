namespace Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
public abstract class IdentifierNode : QueryNode
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public IdentifierNode(string name)
    {
        Name = name;
    }

    /// <summary>
    /// The identifier name.
    /// </summary>
    public string? Name { get; }
}