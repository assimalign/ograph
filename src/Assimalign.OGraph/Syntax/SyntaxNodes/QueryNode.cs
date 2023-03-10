using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax; 

/// <summary>
/// 
/// </summary>
public abstract class QueryNode
{
    /// <summary>
    /// The depth of this node below the root.
    /// </summary>
    //public abstract int Depth { get; init; }

    /// <summary>
    /// An identifier for the node type.
    /// </summary>
    public abstract QueryNodeType NodeType { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="visitor"></param>
    /// <returns></returns>
    public virtual T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    /// <returns></returns>
    public virtual IEnumerable<TNode> GetNodesOfType<TNode>()
    {
        return Array.Empty<TNode>();
    }
}
