using Assimalign.OGraph.Syntax.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
public sealed class RootNode : QueryNode
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="vertex"></param>
    internal RootNode(VertexNode vertex, string text, Location location) 
        : base(text, location)
    {
        if (vertex is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(vertex));
        }
        Vertex = vertex;
    }

    /// <summary>
    /// The starting vertex of query.
    /// </summary>
    public VertexNode Vertex { get; }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Root;

    #region Overloads
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
        if (this is TNode root)
        {
            yield return root;
        }
        else if (Vertex is not null) // There should ever be one Root Node in the Tree. 
        {
            foreach (var node in Vertex.GetNodesOfType<TNode>())
            {
                yield return node;
            }
        }
    }
    #endregion
}
