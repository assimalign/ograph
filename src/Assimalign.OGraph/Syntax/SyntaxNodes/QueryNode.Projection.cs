using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// Unlike select statements, projections are not limited to a single entity.
/// </remarks>
public sealed class ProjectionQueryNode : QueryNode
{
    internal ProjectionQueryNode() { }
    public ProjectionQueryNode(IEnumerable<AttributeQueryNode> attributes)
    {
        this.Attributes = attributes;
    }
    public ProjectionQueryNode(EdgeQueryNode edge, IEnumerable<AttributeQueryNode> attributes)
    {
        this.Edge = edge;
        this.Attributes = attributes;
    }
    /// <summary>
    /// Represents the edge, if any, to apply projections.
    /// </summary>
    public EdgeQueryNode? Edge { get; init; }
    /// <summary>
    /// Specifies whether the projection is the root of the query.
    /// </summary>
    public bool IsRoot => Edge is null;
    /// <summary>
    /// A collection of attributes to project in the query.
    /// </summary>
    public IEnumerable<AttributeQueryNode> Attributes { get; init; } = new AttributeQueryNode[0];

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Projection;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
