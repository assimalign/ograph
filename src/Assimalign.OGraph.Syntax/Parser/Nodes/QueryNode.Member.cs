using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class MemberQueryNode : QueryNode
{
    /// <summary>
    /// Represents the name of the member.
    /// </summary>
    public string? Name { get; init; }
    /// <summary>
    /// Represents nested Identifiers
    /// </summary>
    public IEnumerable<QueryNode>? Children { get; init; } = new QueryNode[0];
    /// <summary>
    /// Specifies whether there are child identifiers.
    /// </summary>
    public bool HasChildren => Children != null && Children.Any();

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Member;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
