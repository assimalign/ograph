using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class SelectQueryNode : QueryNode
{
    private readonly List<QueryNode> paths = new();

    public SelectQueryNode()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<QueryNode> Paths => this.paths;

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Select;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }

    internal void AddPath(QueryNode path) => paths.Add(path);
}
