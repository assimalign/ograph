using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class ProjectionQueryNode : QueryNode
{
    private readonly List<FieldQueryNode> fields = new();

    internal ProjectionQueryNode() { }
    public ProjectionQueryNode(IEnumerable<FieldQueryNode> nodes)
    {
        if (!nodes.Any())
        {
            // TODO: Throw an exception
        }

        this.fields.AddRange(nodes);
    }

    /// <summary>
    /// A collection of fields to project in the query.
    /// </summary>
    public IEnumerable<FieldQueryNode> Fields => this.fields;

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
            fields.Add(member);
        }
    }
}
