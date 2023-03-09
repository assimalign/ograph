using System;
using System.Text;

namespace Assimalign.OGraph.Syntax;

public sealed class PageQueryNode : QueryNode
{
    public PageQueryNode() { }
    public PageQueryNode(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            throw new ArgumentNullException(nameof(token));
        }
        Token = new ConstantQueryNode()
        {
            Value = Encoding.UTF8.GetBytes(token)
        };
    }
    public PageQueryNode(long take, long skip)
    {
        this.Skip = new ConstantQueryNode()
        {
            Value = new byte[1] { (byte)skip }
        };
        this.Take = new ConstantQueryNode()
        {
            Value = new byte[1] { (byte)take }
        };
    }
    public PageQueryNode(EdgeQueryNode edge, long take, long skip) 
        : this(take, skip)
    {
        this.Edge = edge;   
    }


    /// <summary>
    /// Represents the edge, if any, to apply paging.
    /// </summary>
    public EdgeQueryNode? Edge { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public ConstantQueryNode? Take {get; init;}
    /// <summary>
    ///
    /// </summary>
    public ConstantQueryNode? Skip { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public ConstantQueryNode? Token { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public bool HasEdge => Edge is not null;

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Page;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
