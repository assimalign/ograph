using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class FieldQueryNode : QueryNode
{
    public FieldQueryNode() { }
    public FieldQueryNode(QueryNode value, string? alias)
    {
        this.Alias = alias;
        this.Value = value;
    }
    public FieldQueryNode(QueryNode value, IEnumerable<FieldQueryNode> children)
    {
        this.Children = children;
        this.Value = value;
    }
    public FieldQueryNode(QueryNode value, IEnumerable<FieldQueryNode> children, string? alias)
    {
        this.Children = children;
        this.Alias = alias;
        this.Value = value;
    }


    public string Name
    {
        get
        {
            if (!string.IsNullOrEmpty(Alias))
            {
                return Alias;
            }
            if (Value is MemberQueryNode member)
            {
                return member.Name;
            }

            return string.Empty;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public string? Alias { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public QueryNode Value { get; init; }

    /// <summary>
    /// Represents nested Identifiers
    /// </summary>
    public IEnumerable<FieldQueryNode>? Children { get; init; }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Field;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
