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
    public ProjectionQueryNode() { }
    public ProjectionQueryNode(IEnumerable<PropertyQueryNode> properties)
    {
        this.Properties = properties;
    }
    public ProjectionQueryNode(EdgeQueryNode edge, IEnumerable<PropertyQueryNode> properties)
    {
        this.Edge = edge;
        this.Properties = properties;
    }
    /// <summary>
    /// Represents the edge, if any, to apply projections.
    /// </summary>
    public EdgeQueryNode? Edge { get; init; }
    /// <summary>
    /// Specifies whether the projection is the root of the query.
    /// </summary>
    public bool HasEdge => Edge is not null;
    /// <summary>
    /// A collection of properties to project in the query.
    /// </summary>
    public IEnumerable<PropertyQueryNode> Properties { get; init; } = new PropertyQueryNode[0];

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Projection;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
