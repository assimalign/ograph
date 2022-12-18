using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class ProjectionQueryNode : QueryNode
{
    public ProjectionQueryNode() { }
    public ProjectionQueryNode(IEnumerable<FieldQueryNode> fields)
    {
        this.Fields = fields;
    }

    /// <summary>
    /// A collection of fields to project in the query.
    /// </summary>
    public IEnumerable<FieldQueryNode> Fields { get; init; } = new FieldQueryNode[0];

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Projection;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
