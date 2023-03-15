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
    public ProjectionNode() { }
    public ProjectionNode(IEnumerable<PropertyNode> properties)
    {
        this.Properties = properties;
    }
    public ProjectionNode(IEnumerable<PropertyNode> properties, IEnumerable<EdgeProjectionNode> edges)
    {
        this.Properties = properties;
        this.Edges = edges;
    }
      

    /// <summary>
    /// A collection of properties to project in the query.
    /// </summary>
    public IEnumerable<PropertyNode> Properties { get; init; } = new PropertyNode[0];

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<EdgeProjectionNode>? Edges { get; init; }

    /// <summary>
    /// Specifies whether the projection is the root of the query.
    /// </summary>
    public bool HasEdges => Edges is not null && Edges.Any();

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Projection;

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
        if (Edges is not null)
        {
            foreach (var edge in Edges)
            {
                foreach (var node1 in edge.GetNodesOfType<TNode>())
                {
                    yield return node1;
                }
            }
        }
        foreach (var node2 in Properties.SelectMany(x => x.GetNodesOfType<TNode>()))
        {
            yield return node2;
        }
    }
}
