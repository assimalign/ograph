using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public sealed class FilterNode : QueryNode
{
    internal BinaryNode? predicate;

    FilterNode() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <exception cref="ArgumentNullException"/>
    public FilterNode(BinaryNode predicate)
    {
        if (predicate is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(predicate));
        }
        this.predicate = predicate;
    }

    /// <summary>
    /// 
    /// </summary>
    public BinaryNode? Predicate => predicate;

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


    internal static FilterNode Create() => new FilterNode();
}
