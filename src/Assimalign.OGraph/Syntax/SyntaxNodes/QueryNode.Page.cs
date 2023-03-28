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
    /// <param name="take"></param>
    /// <param name="skip"></param>
    /// <param name="identifier"></param>
    public PageNode(long take, long skip, IdentifierNode identifier)
        : this(take, skip)
    {
        this.Identifier = identifier;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="take"></param>
    /// <param name="skip"></param>
    /// <param name="edges"></param>
    public PageNode(long take, long skip, IEnumerable<PageNode> edges) 
        : this(skip, take)
    {
        this.Edges = edges;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="take"></param>
    /// <param name="skip"></param>
    /// <param name="identifier"></param>
    /// <param name="edges"></param>
    public PageNode(long take, long skip, IdentifierNode identifier, IEnumerable<PageNode> edges)
        : this(skip, take, identifier)
    {
        this.Edges = edges;
    }


    /// <summary>
    /// Represents the identifier, if any, to apply paging.
    /// </summary>
    public IdentifierNode? Identifier { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public ConstantNode? Take { get; init; }
    /// <summary>
    ///
    /// </summary>
    public ConstantNode? Skip { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<PageNode> Edges { get; init; } = new PageNode[0];
    /// <summary>
    /// 
    /// </summary>
    public bool HasEdgse => Edges is not null && Edges.Any();

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
        if (Identifier is not null)
        {
            foreach (var item in Identifier.GetNodesOfType<TNode>())
            {
                yield return item;
            }
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
