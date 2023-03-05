using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Assimalign.OGraph.Syntax;

/// <summary>
/// An Attribute node represents a single projection either as a property, function, constant, or binary.
/// <code>
/// // Constant Projection
/// project({
///     5 as ConstantField
/// })
/// </code>
/// </summary>
public sealed class AttributeQueryNode : QueryNode
{
    internal AttributeQueryNode() { }
    public AttributeQueryNode(QueryNode value, string? alias)
    {
        this.Alias = alias;
        this.Value = value;
    }
    public AttributeQueryNode(QueryNode value, IEnumerable<AttributeQueryNode> children)
    {
        this.Children = children;
        this.Value = value;
    }
    public AttributeQueryNode(QueryNode value, IEnumerable<AttributeQueryNode> children, string? alias)
    {
        this.Children = children;
        this.Alias = alias;
        this.Value = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public string? Alias { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public QueryNode? Value { get; init; }

    /// <summary>
    /// Represents nested Identifiers
    /// </summary>
    public IEnumerable<AttributeQueryNode>? Children { get; init; }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Attribute;

    /// <summary>
    /// 
    /// </summary>
    public bool HasChildren => Children is not null && Children.Any();

    /// <summary>
    /// Specifies whether the Projected Node is a constant value.
    /// </summary>
    public bool IsConstant(out ConstantQueryNode? constant)
    {
        constant = default;
        if (Value is ConstantQueryNode cast)
        {
            constant = cast;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool IsBinary(out BinaryQueryNode? binary)
    {
        binary = default;
        if (Value is BinaryQueryNode cast)
        {
            binary = cast;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool IsProperty(out PropertyQueryNode? property)
    {
        property = default;
        if (Value is PropertyQueryNode cast)
        {
            property = cast;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool IsFunction(out FunctionQueryNode? function)
    {
        function = default;
        if (Value is FunctionQueryNode cast)
        {
            function = cast;
            return true;
        }
        return false;
    }

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
