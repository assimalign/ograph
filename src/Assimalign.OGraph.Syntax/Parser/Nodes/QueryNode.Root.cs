using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Assimalign.OGraph.Syntax;

public sealed class RootQueryNode : QueryNode
{
    private readonly List<QueryNode> nodes = new();
    private readonly IDictionary<string, object> variables = new Dictionary<string, object>();

    internal RootQueryNode() { }
    public RootQueryNode(IEnumerable<QueryNode> nodes)
    {
        this.nodes.AddRange(nodes);
    }
    public RootQueryNode(IEnumerable<QueryNode> nodes, IDictionary<string, object> variables)
    {
        this.nodes.AddRange(nodes);
        this.variables = variables;
    }


    /// <summary>
    /// 
    /// </summary>
    public IReadOnlyDictionary<string, object> Variables => this.variables.ToImmutableDictionary();
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
    public bool TryGetSelectNode(out ProjectionQueryNode node) => TryGetNode(out node);
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
        if (node is not FilterQueryNode &&
            node is not ProjectionQueryNode &&
            node is not SortQueryNode &&
            node is not PageQueryNode)
        {
            throw new Exception("");
        }

        nodes.Add(node);
    }
}