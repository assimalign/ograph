using System;

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
            Value = token
        };
    }
    public PageQueryNode(long take, long skip)
    {
        Skip = new ConstantQueryNode()
        {
            Value = skip
        };
        Take = new ConstantQueryNode()
        {
            Value = take
        };
    }


    /// <summary>
    /// Represents the edge, if any, to apply paging.
    /// </summary>
    public string? Edge { get; init; }

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

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Page;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
