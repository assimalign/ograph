using System;

namespace Assimalign.OGraph.Syntax;

public sealed class EdgeQueryNode : QueryNode
{
    internal EdgeQueryNode() { }
    public EdgeQueryNode(string path)
    {
        this.Path = path;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Path { get; init; }

    /// <inheritdoc />
    public override QueryNodeType NodeType => throw new NotImplementedException();

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string[] GetSegments()
    {
        return Path.Split('/');
    }
}
