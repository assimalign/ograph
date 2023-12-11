using System;
using System.Collections.Generic;
using System.Linq;

namespace Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
public sealed class RootNode : QueryNode
{
    public RootNode(IEnumerable<VertexNode> vertices)
    {
        if (vertices is null || !vertices.Any())
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<VertexNode> Vertices { get; init; }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Root;

    /// <inheritdoc />
    public override void Accept(IQueryNodeVisitor visitor)
    {
        visitor.Visit(this);
    }

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }

    public override IEnumerable<TNode> GetNodesOfType<TNode>()
    {
        return base.GetNodesOfType<TNode>();
    }
}
