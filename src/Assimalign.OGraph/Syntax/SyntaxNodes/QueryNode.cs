using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax; 

/// <summary>
/// 
/// </summary>
public abstract class QueryNode
{
    /// <summary>
    /// An identifier for the node type.
    /// </summary>
    public abstract QueryNodeType NodeType { get; }
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
    /// <summary>
    /// Checks whether the node is of type: <typeparamref name="TNode"/>.
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <returns></returns>
    public virtual bool IsOfType<TNode>() where TNode : QueryNode
    {
        return this is TNode;
    }
}
