using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Assimalign.OGraph.Syntax;

/// <summary>
/// The query tree represents the complete parsed expression tree.
/// </summary>
public sealed class QueryTree
{
    internal QueryTree() { }

    internal QueryTree(IEnumerable<QueryNode> nodes)
    {
        this.Nodes = nodes;
    }

    /// <summary>
    /// Represents the root nodes of the expression tree.
    /// </summary>
    public IEnumerable<QueryNode> Nodes { get; init; }
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
}
