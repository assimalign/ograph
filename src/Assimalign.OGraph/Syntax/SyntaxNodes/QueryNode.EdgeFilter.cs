using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class EdgeFilterNode : EdgeNode
{
    public EdgeFilterNode() { }
    public EdgeFilterNode(BinaryNode predicate)
    {
        Predicate = predicate;
    }
    public EdgeFilterNode(BinaryNode predicate, IEnumerable<EdgeFilterNode> edges)
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
