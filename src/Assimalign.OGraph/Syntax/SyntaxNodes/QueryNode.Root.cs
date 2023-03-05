using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Xml.Linq;

namespace Assimalign.OGraph.Syntax;

public sealed class RootQueryNode : QueryNode
{
    private IEnumerable<QueryNode> nodes;

    public RootQueryNode() { }
    public RootQueryNode(IEnumerable<QueryNode> nodes)
    {
        this.Nodes = nodes;
    }
    public RootQueryNode(IEnumerable<QueryNode> nodes, IDictionary<string, object> variables)
    {
        this.Nodes = nodes;
        this.Variables = variables.ToImmutableDictionary();
    }

    /// <summary>
    /// 
    /// </summary>
    public IReadOnlyDictionary<string, object> Variables { get; init; } = new Dictionary<string, object>();
    /// <summary>
    /// Represents the root nodes of the expression tree.
    /// </summary>
    public IEnumerable<QueryNode> Nodes { get; init; } = new QueryNode[0];



    public bool TryGetProjections(out IEnumerable<ProjectionQueryNode> nodes) => TryGetNodes(out nodes);
    public bool TryGetFilters(out IEnumerable<FilterQueryNode> nodes) => TryGetNodes(out nodes);
    public bool TryGetSorts(out IEnumerable<SortQueryNode> nodes) => TryGetNodes(out nodes);
    public bool TryGetPages(out IEnumerable<PageQueryNode> nodes) => TryGetNodes(out nodes);

    
    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Root;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }


    private bool TryGetNodes<TNode>(out IEnumerable<TNode> nodes)
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