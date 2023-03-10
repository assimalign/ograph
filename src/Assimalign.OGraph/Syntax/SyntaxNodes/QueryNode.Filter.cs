using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class FilterQueryNode : QueryNode
{
    public FilterQueryNode() { }
    public FilterQueryNode(BinaryQueryNode predicate)
    {
        Predicate = predicate;
    }
    public FilterQueryNode(EdgeQueryNode? edge, BinaryQueryNode predicate)
    {
        Edge = edge;
        Predicate = predicate;
    }


    /// <summary>
    /// Represents the edge, if any, to apply filter.
    /// </summary>
    public EdgeQueryNode? Edge { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public BinaryQueryNode? Predicate { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public bool HasEdge => Edge is not null;

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Filter;

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
        if (Edge is not null)
        {
            foreach (var item in Edge.GetNodesOfType<TNode>())
            {
                yield return item;
            }
        }
        if (Predicate is not null)
        {
            foreach (var item in Predicate.GetNodesOfType<TNode>())
            {
                yield return item;
            }
        }
    }
}
