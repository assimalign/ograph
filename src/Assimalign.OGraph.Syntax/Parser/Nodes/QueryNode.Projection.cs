using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class ProjectionQueryNode : QueryNode
{
    private readonly List<FieldQueryNode> members = new();

    public ProjectionQueryNode()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<FieldQueryNode> Projections => this.members;

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Projection;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }

    internal void AddProjection(QueryNode node)
    {
        if (node is FieldQueryNode member)
        {
            members.Add(member);
        }
    }
}
