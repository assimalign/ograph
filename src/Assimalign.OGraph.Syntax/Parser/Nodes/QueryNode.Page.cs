using System;

namespace Assimalign.OGraph.Syntax;

public sealed class PageQueryNode : QueryNode
{
    private ConstantQueryNode? take;
    private ConstantQueryNode? skip;
    private ConstantQueryNode? token;

    /// <summary>
    /// 
    /// </summary>
    public ConstantQueryNode? Take => this.take;
    /// <summary>
    ///
    /// </summary>
    public ConstantQueryNode? Skip => this.skip;
    /// <summary>
    /// 
    /// </summary>
    public ConstantQueryNode? Token => this.token;

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Page;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }

    internal void SetTake(ConstantQueryNode? take) => this.take = take;
    internal void SetSkip(ConstantQueryNode? skip) => this.skip = skip;
    internal void SetToken(ConstantQueryNode token) => this.token = token;
}
