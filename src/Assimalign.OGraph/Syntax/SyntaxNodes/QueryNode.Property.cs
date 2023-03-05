using System;

namespace Assimalign.OGraph.Syntax;

public sealed class PropertyQueryNode : QueryNode
{
    internal PropertyQueryNode() { }

    public PropertyQueryNode(string name)
    {
        this.Name = name;
    }

    /// <summary>
    /// Represents the name of the member.
    /// </summary>
    public string? Name { get; init; }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Property;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
