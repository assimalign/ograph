using System;
using System.Collections.Generic;
using System.Linq;

namespace Assimalign.OGraph.Syntax;

public sealed class PropertyQueryNode : QueryNode
{
    public PropertyQueryNode() { }
    public PropertyQueryNode(string name)
    {
        this.Name = name;
    }
    
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
    public IEnumerable<PropertyQueryNode>? Children { get; init; }
    /// <summary>
    /// Specifies whether the Attribute has nested Identifiers.
    /// </summary>
    public bool HasChildren => Children is not null && Children.Any();

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Property;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
