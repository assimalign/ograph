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

    internal IdentifierNode(string name, string text, Location location) 
        : base(text, location)
    {
        Name = name;
    }

    /// <summary>
    /// The identifier name.
    /// </summary>
    public string? Name { get; }
}