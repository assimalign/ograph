using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class SortQueryNode : QueryNode
{
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<QueryNode> Paths { get; init; }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Sort;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
