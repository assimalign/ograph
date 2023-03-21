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
        this.Properties = properties;
    }
    /// <summary>
    /// A default constructor for <see cref="ProjectionNode"/>.
    /// </summary>
    /// <param name="properties"></param>
    /// <param name="identifier"></param>
    public ProjectionNode(IEnumerable<PropertyNode> properties, IdentifierNode identifier) 
        : this(properties)
    {
        this.Identifier = identifier;
    }
    /// <summary>
    /// A default constructor for <see cref="ProjectionNode"/>.
    /// </summary>
    /// <param name="properties"></param>
    /// <param name="edges"></param>
    public ProjectionNode(IEnumerable<PropertyNode> properties, IEnumerable<ProjectionNode> edges) 
        : this(properties)
    {
        this.Edges = edges;
    }
    /// <summary>
    /// A default constructor for <see cref="ProjectionNode"/>.
    /// </summary>
    /// <param name="properties"></param>
    /// <param name="edges"></param>
    /// <param name="identifier"></param>
    public ProjectionNode(IEnumerable<PropertyNode> properties, IEnumerable<ProjectionNode> edges, IdentifierNode identifier) 
        : this(properties, edges)
    {
        this.Identifier = identifier;
    }
      
    /// <summary>
    /// Represents the identifier of the projection.
    /// </summary>
    /// <remarks>
    /// This is used to identify the projection in the 
    /// query chain and should always be a edge identifier.
    /// </remarks>
    public IdentifierNode? Identifier { get; init; }

    /// <summary>
    /// A collection of properties to project in the query.
    /// </summary>
    public IEnumerable<PropertyNode> Properties { get; init; } = new PropertyNode[0];

    /// <summary>
    /// Represents edge projections via the query chain.
    /// </summary>
    public IEnumerable<ProjectionNode> Edges { get; init; } = new ProjectionNode[0];

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
