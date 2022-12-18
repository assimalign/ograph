using System;
using System.Collections.Generic;
using System.Linq;

namespace Assimalign.OGraph.Syntax;

public sealed class SortQueryNode : QueryNode
{
    private readonly List<FieldQueryNode> fields = new();

    internal SortQueryNode() { }
    public SortQueryNode(IEnumerable<FieldQueryNode> nodes)
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
    public override QueryNodeType NodeType => QueryNodeType.Sort;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
