using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class RootNode : QueryNode
{
    public RootNode() { }
    public RootNode(IEnumerable<QueryNode> nodes)
    {
        this.Nodes = nodes;
    }

   
    /// <summary>
    /// Represents the root edges of the queryable tree.
    /// </summary>
    public IEnumerable<QueryNode> Nodes { get; init; } = new QueryNode[0];
    /// <summary>
    /// 
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    public bool TryGetProjection(out ProjectionNode? node) => TryGetNode(out node);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    public bool TryGetFilter(out FilterNode? node) => TryGetNode(out node);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    public bool TryGetSort(out SortNode? node) => TryGetNode(out node);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    public bool TryGetPage(out PageNode? node) => TryGetNode(out node);

    
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

    /// <inheritdoc />
    public override IEnumerable<TNode> GetNodesOfType<TNode>()
    {
        if (this is TNode node1)
        {
            yield return node1;
        }
        foreach (var node in Nodes)
        {
            foreach (var node2 in node.GetNodesOfType<TNode>())
            {
                yield return node2;
            }
        }
    }

    private bool TryGetNode<TNode>(out TNode? node)
    {
        node = default;

        foreach (var n in Nodes)
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