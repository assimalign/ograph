using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class PageNode : QueryNode
{
    internal PageNode() { }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="take"></param>
    /// <param name="skip"></param>
    public PageNode(long take, long skip)
    {
        this.Skip = new ConstantNode(BitConverter.GetBytes(skip));
        this.Take = new ConstantNode(BitConverter.GetBytes(take));
    }
 
    /// <summary>
    /// 
    /// </summary>
    public ConstantNode? Take { get; init; }
    /// <summary>
    ///
    /// </summary>
    public ConstantNode? Skip { get; init; }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Page;

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
}
