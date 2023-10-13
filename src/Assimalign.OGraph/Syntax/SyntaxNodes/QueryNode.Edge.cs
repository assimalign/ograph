using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
public sealed class EdgeNode : QueryNode
{
    private readonly IEnumerable<QueryNode>? nodes;

    internal EdgeNode() { }
    public EdgeNode(IEnumerable<QueryNode> nodes)
    {
        this.Nodes = nodes ?? new QueryNode[0];
    }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Edge;

    /// <summary>
    /// The edge name to be invoked.
    /// </summary>
    public IdentifierNode? Identifier { get; init; }

    /// <summary>
    /// Gets the projections, filtering, paging, and 
    /// sorting of the edge as well as any nested edges.
    /// </summary>
    public IEnumerable<QueryNode>? Nodes
    {
        get => nodes;
        init
        {
            var isValid = value.Any(node =>
                node is not ProjectionNode &&
                node is not FilterNode &&
                node is not SortNode &&
                node is not PageNode &&
                node is not EdgeNode);

            if (!isValid)
            {
                throw new InvalidOperationException("Wrong node");
            }

            nodes = value;
        }
    }

    /// <summary>
    /// Returns
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public bool TryGetProjection(out ProjectionNode? node) => TryGetNode(out node);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public bool TryGetFilter(out FilterNode? node) => TryGetNode(out node);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public bool TryGetSort(out SortNode? node) => TryGetNode(out node);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    public bool TryGetPage(out PageNode? node) => TryGetNode(out node);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="edges"></param>
    /// <returns></returns>
    public bool TryGetEdges(out IEnumerable<EdgeNode> edges)
    {
        edges = Nodes!.OfType<EdgeNode>();

        if (edges.Any())
        {
            return true;
        }

        return false;
    }

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

    private bool TryGetNode<TNode>(out TNode? node)
    {
        node = default;

        foreach (var n in Nodes!)
        {
            if (n is TNode tn)
            {
                node = tn;
                return true;
            }
        }

        return false;
    }
}
