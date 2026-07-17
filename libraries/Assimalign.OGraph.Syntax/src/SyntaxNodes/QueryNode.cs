using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax; 

/// <summary>
/// The base class for a syntax node.
/// </summary>
public abstract class QueryNode
{
    internal QueryNode() { }
    internal QueryNode(string? text, Location location)
    {
        Text = text;
        Location = location;
    }

    /// <summary>
    /// An identifier for the node type.
    /// </summary>
    public abstract QueryNodeType NodeType { get; }

    /// <summary>
    /// The raw text of the node.
    /// </summary>
    public virtual string? Text { get; }

    /// <summary>
    /// The location of a query node.
    /// </summary>
    public virtual Location Location { get; }

    /// <summary>
    /// Accepts a visitor.
    /// </summary>
    /// <param name="visitor"></param>
    public virtual void Accept(IQueryNodeVisitor visitor)
    {
        visitor.Visit(this);
    }

    /// <summary>
    /// Accepts an <see cref="IQueryNodeVisitor{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="visitor"></param>
    /// <returns></returns>
    public virtual T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }

    /// <summary>
    /// Returns a collection of nodes within the tree that match the type of: <typeparamref name="TNode"/>.
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <returns></returns>
    public virtual IEnumerable<TNode> GetNodesOfType<TNode>() where TNode : QueryNode
    {
        return Array.Empty<TNode>();
    }
}