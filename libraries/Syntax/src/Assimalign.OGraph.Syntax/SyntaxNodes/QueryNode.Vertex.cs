using System;
using System.Collections.Generic;
using System.Linq;

namespace Assimalign.OGraph.Syntax;

/// <summary>
/// The vertex node represents the root or starting vertices in the ograph query.
/// </summary>
public sealed class VertexNode : QueryNode
{
    public VertexNode() { }
    public VertexNode(IEnumerable<QueryNode> nodes)
    {
        this.Nodes = nodes;
    }

    /// <summary>
    /// Specifies whether this vertex node is the root node.
    /// </summary>
    public bool IsRoot { get; init; }
    /// <summary>
    /// The vertex identifier
    /// </summary>
    public LabelNode Label { get; init; }
    /// <summary>
    /// Represents the root edges of the queryable tree.
    /// </summary>
    public IEnumerable<QueryNode> Nodes { get; init; } = new QueryNode[0];

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Vertex;

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
        if (this is TNode node1)
        {
            yield return node1;
        }
        foreach (var node in Nodes)
        {
            foreach (var node2 in node.GetNodesOfType<TNode>())
            {
                yield return node2;
            }
        }
    }



    public IEnumerable<EdgeNode> GetEdgeNodes()
    {
        foreach (var node in Nodes)
        {
            if (node is EdgeNode edge)
            {
                yield return edge;
            }
        }
    }

    private bool TryGetNode<TNode>(out TNode? node)
    {
        node = default;

        foreach (var n in Nodes)
        {
            if (n is TNode tn)
            {
                node = tn;
                return true;
            }
        }

        return false;
    }
}