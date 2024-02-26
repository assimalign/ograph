using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

/// <summary>
/// 
/// </summary>
public sealed class EdgeNode : QueryNode
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="alias"></param>
    /// <exception cref="ArgumentNullException" />
    public EdgeNode(LabelNode label, VertexNode source, VertexNode target, LabelNode? alias = null, string? path = null) 
    {
        if (label is null) ThrowHelper.ThrowArgumentNullException(nameof(label));
        if (source is null) ThrowHelper.ThrowArgumentNullException(nameof(source));
        if (target is null) ThrowHelper.ThrowArgumentNullException(nameof(target));
       
        Label = label;
        Source = source;
        Target = target;
        Alias = alias;
        Path = path;
    }

    /// <summary>
    /// The edge label.
    /// </summary>
    public LabelNode Label { get; }
    /// <summary>
    /// The source vertex.
    /// </summary>
    public VertexNode? Source { get; }
    /// <summary>
    /// The target vertex.
    /// </summary>
    public VertexNode? Target { get; }
    /// <summary>
    /// A temporary name to be assigned in replacement of the property name.
    /// </summary>
    public LabelNode? Alias { get; }
    /// <summary>
    /// 
    /// </summary>
    public override string? Path { get; }
    /// <summary>
    /// 
    /// </summary>
    public bool HasAlias => Alias is not null;

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
        if (this is TNode edge)
        {
            yield return edge;
        }
        foreach (var node in Label.GetNodesOfType<TNode>())
        {
            yield return node;
        }
        foreach (var node1 in Source.GetNodesOfType<TNode>())
        {
            yield return node1;
        }
        foreach (var node2 in Target.GetNodesOfType<TNode>())
        {
            yield return node2;
        }
    }
}