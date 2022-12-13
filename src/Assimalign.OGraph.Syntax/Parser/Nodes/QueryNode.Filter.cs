using System;

namespace Assimalign.OGraph.Syntax;

public sealed class FilterQueryNode : QueryNode
{
    /// <summary>
    /// 
    /// </summary>
    public QueryNode Predicate { get; init; }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Filter;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
