using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class FieldQueryNode : QueryNode
{
    private readonly List<FieldQueryNode> children = new();

    /// <summary>
    /// 
    /// </summary>
    public string? Alias { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public QueryNode Value { get; set; }

    /// <summary>
    /// Represents nested Identifiers
    /// </summary>
    public IEnumerable<FieldQueryNode>? Children => this.children;

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Field;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
