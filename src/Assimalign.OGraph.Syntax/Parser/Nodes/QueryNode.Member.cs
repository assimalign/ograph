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
    /// 
    /// </summary>
    public string? Alias { get; init; }
    /// <summary>
    /// Represents nested Identifiers
    /// </summary>
    public IEnumerable<MemberQueryNode>? Children { get; init; } = new MemberQueryNode[0];
    /// <summary>
    /// 
    /// </summary>
    public FunctionCallQueryNode? Function { get; init; }
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
