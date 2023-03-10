using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class EdgeQueryNode : QueryNode
{
    public EdgeQueryNode() { }
    public EdgeQueryNode(string path)
    {
        this.Path = path;
    }

    /// <summary>
    /// 
    /// </summary>
    public string? Path { get; init; }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Edge;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }

    /// <inheritdoc />
    public override IEnumerable<TNode> GetNodesOfType<TNode>()
    {
        if (this is TNode node)
        {
            yield return node;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string[] GetSegments()
    {
        return Path?.Split('/') ?? new string[0];
    }
}
