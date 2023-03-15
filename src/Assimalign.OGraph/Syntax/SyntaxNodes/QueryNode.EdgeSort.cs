using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed class EdgeSortNode : EdgeNode
{
    public EdgeSortNode() { }

    public EdgeSortNode(QueryNode sortBy)
    {
        this.SortBy = sortBy;
    }
    public EdgeSortNode(QueryNode sortBy, EdgeSortNode thenBy)
    {
        this.SortBy = sortBy;
        this.ThenBy = thenBy;
    }
    public EdgeSortNode(QueryNode sortBy, EdgeSortNode thenBy, IEnumerable<EdgeSortNode> edges)
    {
        this.SortBy = sortBy;
        this.ThenBy = thenBy;
        this.Edges = edges;
    }
    public EdgeSortNode(QueryNode sortBy, IEnumerable<EdgeSortNode> edges)
    {
        this.SortBy = sortBy;
        this.Edges = edges;
    }


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
    public EdgeSortNode? ThenBy { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<EdgeSortNode>? Edges { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public bool HasEdges => Edges is not null && Edges.Any();
    /// <summary>
    /// 
    /// </summary>
    public bool HasThenBy => ThenBy is not null;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="function"></param>
    /// <returns></returns>
    public bool IsFunction(out FunctionQueryNode? function)
    {
        function = null;

        if (SortBy is FunctionQueryNode functionNode)
        {
            function = functionNode;
            return true;
        }

        return false;
    }


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
