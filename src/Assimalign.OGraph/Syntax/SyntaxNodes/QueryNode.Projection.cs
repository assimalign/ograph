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
public sealed class ProjectionNode : QueryNode
{
    internal ProjectionNode() { }
    /// <summary>
    /// A default constructor for <see cref="ProjectionNode"/>.
    /// </summary>
    /// <param name="properties"></param>
    public ProjectionNode(IEnumerable<PropertyNode> properties)
    {
        if (properties is null || !properties.Any())
        {
            throw new ArgumentNullException(nameof(properties), $"{nameof(properties)} cannot be empty.");
        }
        Properties = properties;
    }
      

    /// <summary>
    /// A collection of properties to project in the query results.
    /// </summary>
    public IEnumerable<PropertyNode> Properties { get; init; } = new PropertyNode[0];

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Projection;

    /// <inheritdoc />
    public override void Accept(IQueryNodeVisitor visitor)
    {
        visitor.Visit(this);
    }

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }

    /// <inheritdoc />
    public override IEnumerable<TNode> GetNodesOfType<TNode>()
    {
        if (this is TNode node)
        {
            yield return node;
        }
        foreach (var node2 in Properties.SelectMany(x => x.GetNodesOfType<TNode>()))
        {
            yield return node2;
        }
    }
}
