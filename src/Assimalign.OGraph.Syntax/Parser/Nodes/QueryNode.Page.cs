using System;

namespace Assimalign.OGraph.Syntax;

public sealed class PageQueryNode : QueryNode
{
    private long? take;
    private long? skip;
    private string? token;

    /// <summary>
    /// 
    /// </summary>
    public long? Take => this.take;
    /// <summary>
    ///
    /// </summary>
    public long? Skip => this.skip;
    /// <summary>
    /// 
    /// </summary>
    public string? Token => this.token;

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Page;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }

    internal void SetTake(long? take) => this.take = take;
    internal void SetSkip(long? skip) => this.skip = skip;
    internal void SetToken(string token) => this.token = token;
}
