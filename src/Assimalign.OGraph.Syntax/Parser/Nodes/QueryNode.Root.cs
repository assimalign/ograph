using System;
using System.Collections.Generic;
using System.Linq;

namespace Assimalign.OGraph.Syntax;

public sealed class RootQueryNode : QueryNode
{
    private readonly IList<QueryNode> nodes = new List<QueryNode>();

    internal RootQueryNode() { }

    /// <summary>
    /// Represents the root nodes of the expression tree.
    /// </summary>
    public IEnumerable<QueryNode> Nodes => this.nodes;
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool TryGetFilterNode(out FilterQueryNode node) => TryGetNode(out node);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool TryGetSelectNode(out SelectQueryNode node) => TryGetNode(out node);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool TryGetSortNode(out SortQueryNode node) => TryGetNode(out node);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool TryGetPageNode(out PageQueryNode node) => TryGetNode(out node);
    
    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Root;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }

    private bool TryGetNode<TNode>(out TNode node)
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


    internal void AddNode(QueryNode node)
    {
        if (node is not FilterQueryNode ||
            node is not SelectQueryNode ||
            node is not SortQueryNode ||
            node is not PageQueryNode)
        {
            throw new Exception("");
        }

        nodes.Add(node);
    }
}