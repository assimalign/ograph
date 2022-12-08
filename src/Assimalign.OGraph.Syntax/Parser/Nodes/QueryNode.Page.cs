using System;

namespace Assimalign.OGraph.Syntax;

public sealed class PageQueryNode : QueryNode
{
    /// <summary>
    /// 
    /// </summary>
    public int? Take { get; init; }

    /// <summary>
    ///
    /// </summary>
    public int? Skip { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public string? Token { get; init; }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Page;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
