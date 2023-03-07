using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class SortQueryNode : QueryNode
{
    internal SortQueryNode() { }
    public SortQueryNode(IEnumerable<AttributeQueryNode> attributes)
    {
        if (!attributes.Any())
        {
            // TODO: Throw an exception
        }

        this.Attributes = attributes;
    }
    public SortQueryNode(EdgeQueryNode edge, IEnumerable<AttributeQueryNode> attributes)
    {
        this.Edge = edge;
        this.Attributes = attributes;
    }

    /// <summary>
    /// Represents the edge, if any, to apply sorting.
    /// </summary>
    public EdgeQueryNode? Edge { get; init; }
    /// <summary>
    /// A collection of attributes to project in the query.
    /// </summary>
    public IEnumerable<AttributeQueryNode>? Attributes { get; init; }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Sort;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
