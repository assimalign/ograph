
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

/// <summary>
/// The vertex node represents the root or starting vertices in the ograph query.
/// </summary>
public sealed class VertexNode : QueryNode
{
    private readonly List<QueryNode> nodes;

    /// <summary>
    /// 
    /// </summary>
    public VertexNode()
    {
        nodes = new List<QueryNode>();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="nodes"></param>
    /// <exception cref="ArgumentNullException" />
    public VertexNode(IEnumerable<QueryNode> nodes)
    {
        if (nodes is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(nodes));
        }
        this.nodes = new List<QueryNode>(nodes);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="argument">Represents a key of a single vertex. Example: vertex('{vertex label}', {argument})</param>
    /// <param name="nodes"></param>
    /// <exception cref="ArgumentNullException" />
    public VertexNode(ConstantNode argument, IEnumerable<QueryNode> nodes) 
        : this(nodes)
    {
        if (argument is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(argument));
        }
        Argument = argument;
    }

    /// <summary>
    /// Represents a key of a single vertex. Example: vertex('{vertex label}', {argument})
    /// </summary>
    public ConstantNode? Argument { get; }

    /// <summary>
    /// Represents the root edges of the queryable tree.
    /// </summary>
    public IEnumerable<QueryNode> Nodes =>
#if NET7_0_OR_GREATER
        nodes.AsReadOnly();
#else
        new ReadOnlyCollection<QueryNode>(nodes);
#endif

    /// <summary>
    /// Checks whether the vertex has a parameter. 
    /// </summary>
    public bool HasArgument => Argument is not null;

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Vertex;

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
        if (this is TNode vertex)
        {
            yield return vertex;
        }
        foreach (var node in Nodes)
        {
            foreach (var child in node.GetNodesOfType<TNode>())
            {
                yield return child;
            }
        }
    }
    #endregion

    internal void AddNode(QueryNode queryNode)
    {
        this.nodes.Add(queryNode);
    }
}