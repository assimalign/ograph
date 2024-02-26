using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
public sealed class PageNode : QueryNode
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    public PageNode(ConstantNode skip, ConstantNode take)
    {
        Skip = skip;
        Take = take;
    }

    /// <summary>
    /// 
    /// </summary>
    public ConstantNode? Take { get; }
    /// <summary>
    ///
    /// </summary>
    public ConstantNode? Skip { get; }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Page;

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
        if (this is TNode node)
        {
            yield return node;
        }
        if (Take is not null)
        {
            foreach (var item in Take.GetNodesOfType<TNode>())
            {
                yield return item;
            }
        }
        if (Skip is not null)
        {
            foreach (var item in Skip.GetNodesOfType<TNode>())
            {
                yield return item;
            }
        }
    }
    #endregion

}
