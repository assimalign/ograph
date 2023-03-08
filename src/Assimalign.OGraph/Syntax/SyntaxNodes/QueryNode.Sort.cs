using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class SortQueryNode : QueryNode
{
    public SortQueryNode() { }
   

    /// <summary>
    /// Represents the edge, if any, to apply sorting.
    /// </summary>
    public EdgeQueryNode? Edge { get; init; }
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
    public SortQueryNode? ThenBy { get; init; }
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
}
