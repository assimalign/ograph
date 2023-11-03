using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
public sealed class EdgeNode : QueryNode
{
    internal EdgeNode() { }
    public EdgeNode(LabelNode labelNode, IEnumerable<VertexNode> vertexNodes)
    {
        this.Label = labelNode;
        this.Vertices = vertexNodes;
    }

    /// <summary>
    /// The edge identifier.
    /// </summary>
    public LabelNode Label { get; init; }
    /// <summary>
    /// The target vertex
    /// </summary>
    public IEnumerable<VertexNode> Vertices { get; init; }
    /// <summary>
    /// A temporary name to be assigned in replacement of the property name.
    /// </summary>
    public string? Alias { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public bool HasAlias => !string.IsNullOrEmpty(Alias);

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Edge;

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
        if (this is TNode n1) yield return n1;
        if (Label is TNode n2) yield return n2;
        foreach (var v in Vertices)
        {
            if (v is TNode n3) yield return n3;
        }
    }
}