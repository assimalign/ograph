using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class SortNode : QueryNode
{
    public SortNode() { }

    public SortNode(QueryNode sortBy)
    {
        this.SortBy = sortBy;
    }
    public SortNode(QueryNode sortBy, SortNode thenBy)
    {
        this.SortBy = sortBy;
        this.ThenBy = thenBy;
    }
    public SortNode(QueryNode sortBy, SortNode thenBy, IEnumerable<SortNode> edges)
    {
        this.SortBy = sortBy;
        this.ThenBy = thenBy;
        this.Edges = edges;
    }
    public SortNode(QueryNode sortBy, IEnumerable<SortNode> edges)
    {
        this.SortBy = sortBy;
        this.Edges = edges;
    }

    /// <summary>
    /// 
    /// </summary>
    public IdentifierNode? Identifier { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public SortDirection Direction { get; init; } = SortDirection.Ascending;

    /// <summary>
    /// 
    /// </summary>
    public QueryNode? SortBy { get; init; }    
    
    /// <summary>
    /// 
    /// </summary>
    public SortNode? ThenBy { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<SortNode>? Edges { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public bool HasEdges => Edges is not null && Edges.Any();

    /// <summary>
    /// 
    /// </summary>
    public bool HasThenBy => ThenBy is not null;   

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Sort;

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
                foreach (var node1 in edge.GetNodesOfType<TNode>())
                {
                    yield return node1;
                }
            }
        }
        if (SortBy is not null)
        {
            foreach (var node1 in SortBy.GetNodesOfType<TNode>())
            {
                yield return node1;
            }
        }
        if (ThenBy is not null)
        {
            foreach (var node1 in ThenBy.GetNodesOfType<TNode>())
            {
                yield return node1;
            }
        }
    }
}
