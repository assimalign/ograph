using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class RootQueryNode : QueryNode
{
    public RootQueryNode() { }
    public RootQueryNode(IEnumerable<QueryNode> nodes)
    {
        this.Nodes = nodes;
    }

   
    /// <summary>
    /// Represents the root nodes of the expression tree.
    /// </summary>
    public IEnumerable<QueryNode> Nodes { get; init; } = new QueryNode[0];
    /// <summary>
    /// 
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    public bool TryGetProjections(out IEnumerable<ProjectionQueryNode>? nodes) => TryGetNodes(out nodes);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    public bool TryGetFilters(out IEnumerable<FilterQueryNode>? nodes) => TryGetNodes(out nodes);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    public bool TryGetSorts(out IEnumerable<SortQueryNode>? nodes) => TryGetNodes(out nodes);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    public bool TryGetPages(out IEnumerable<PageQueryNode>? nodes) => TryGetNodes(out nodes);

    
    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Root;

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

    private bool TryGetNodes<TNode>(out IEnumerable<TNode>? nodes)
    {
        nodes = default;

        var list = new List<TNode>();

        foreach (var n in Nodes)
        {
            if (n is TNode tn)
            {
                list.Add(tn);
            }
        }
        if (list.Count > 0)
        {
            nodes = list.ToArray();
            return true;
        }

        return false;
    }
}