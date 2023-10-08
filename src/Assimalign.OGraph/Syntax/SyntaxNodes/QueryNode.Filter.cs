using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class FilterNode : QueryNode
{
    internal FilterNode() { }
    public FilterNode(BinaryNode predicate)
    {
        Predicate = predicate;
    }

    public FilterNode(BinaryNode predicate, IdentifierNode identifier)
    {
        Predicate = predicate;
        Identifier = identifier;
    }

    /// <summary>
    /// 
    /// </summary>
    public IdentifierNode? Identifier { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public BinaryNode? Predicate { get; init; }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Filter;

    /// <inheritdoc />
    public override void Accept(IQueryNodeVisitor visitor)
    {
        visitor.Visit(this);
    }

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }

    /// <inheritdoc />
    public override IEnumerable<TNode> GetNodesOfType<TNode>()
    {
        if (this is TNode node)
        {
            yield return node;
        }      
        if (Predicate is not null)
        {
            foreach (var item in Predicate.GetNodesOfType<TNode>())
            {
                yield return item;
            }
        }
    }
}
