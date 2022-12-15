using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class FieldQueryNode : QueryNode
{
    private string alias;
    private QueryNode value;

    private readonly List<FieldQueryNode> children = new();

    internal FieldQueryNode() { }
    public FieldQueryNode(QueryNode value, string? alias)
    {
        this.alias = alias;
        this.value = value;
    }
    public FieldQueryNode(QueryNode value, IEnumerable<FieldQueryNode> children)
    {
        this.children.AddRange(children);
        this.value = value;
    }
    public FieldQueryNode(QueryNode value, IEnumerable<FieldQueryNode> children, string? alias)
    {
        this.children.AddRange(children);
        this.alias = alias;
        this.value = value;
    }


    /// <summary>
    /// 
    /// </summary>
    public string? Alias => this.alias;

    /// <summary>
    /// 
    /// </summary>
    public QueryNode Value => this.value;

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

    internal void SetValue(QueryNode node) => this.value = node;
    internal void SetAlias(string alias) => this.alias = alias;
    

    internal void AddChild(QueryNode child)
    {
        if (child is FieldQueryNode field)
        {
            this.children.Add(field);   
        }
        else
        {
            throw new Exception();
        }
    }
}
