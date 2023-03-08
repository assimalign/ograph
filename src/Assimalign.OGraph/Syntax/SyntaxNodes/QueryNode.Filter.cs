using System;

namespace Assimalign.OGraph.Syntax;

public sealed class FilterQueryNode : QueryNode
{
    public FilterQueryNode() { }
    public FilterQueryNode(BinaryQueryNode predicate)
    {
        Predicate = predicate;
    }
    public FilterQueryNode(EdgeQueryNode? edge, BinaryQueryNode predicate)
    {
        Edge = edge;
        Predicate = predicate;
    }


    /// <summary>
    /// Represents the edge, if any, to apply filter.
    /// </summary>
    public EdgeQueryNode? Edge { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public BinaryQueryNode? Predicate { get; init; }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Filter;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
