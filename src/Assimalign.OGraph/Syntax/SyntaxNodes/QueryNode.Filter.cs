using System;
using System.Collections.Generic;
using System.Linq;

namespace Assimalign.OGraph.Syntax;

public sealed class FilterNode : QueryNode
{
    public FilterNode() { }
    public FilterNode(BinaryNode predicate)
    {
        Predicate = predicate;
    }
    public FilterNode(BinaryNode predicate, IEnumerable<EdgeFilterNode> edges)
    {
        Edges = edges;
        Predicate = predicate;
    }


    /// <summary>
    /// 
    /// </summary>
    public BinaryNode? Predicate { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<EdgeFilterNode>? Edges { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public bool HaseEdges => Edges is not null && Edges.Any();

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
        if (Edges is not null)
        {
            foreach (var edge in Edges)
            {
                foreach (var item in edge.GetNodesOfType<TNode>())
                {
                    yield return item;
                }
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
