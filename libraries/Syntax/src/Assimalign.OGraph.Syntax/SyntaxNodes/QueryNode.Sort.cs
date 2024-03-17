using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public sealed class SortNode : QueryNode
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="property"></param>
    /// <exception cref="ArgumentNullException"/>
    public SortNode(PropertyNode property)
    {
        if (property is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(property));
        }
        Identifier = property;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="property"></param>
    /// <param name="direction"></param>
    public SortNode(PropertyNode property, SortDirection direction) : this(property)
    {
        Direction = direction;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="property"></param>
    /// <param name="direction"></param>
    /// <param name="thenBy"></param>
    public SortNode(PropertyNode property, SortDirection direction, SortNode thenBy) : this(property, direction)
    {
        Direction = direction;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="function"></param>
    public SortNode(FunctionCallNode function)
    {
        if (function is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(function));
        }
        Identifier = function;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="functionCall"></param>
    /// <param name="direction"></param>
    /// <exception cref="ArgumentNullException"/>
    public SortNode(FunctionCallNode functionCall, SortDirection direction) 
        : this(functionCall)
    {
        Direction = direction;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="functionCall"></param>
    /// <param name="direction"></param>
    /// <param name="thenBy"></param>
    /// <exception cref="ArgumentNullException"/>
    public SortNode(FunctionCallNode functionCall, SortDirection direction, SortNode thenBy)
        : this(functionCall, direction)
    {
        if (thenBy is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(thenBy));
        }
        ThenBy = thenBy;
    }

    /// <summary>
    /// Represents the property or function output to sort by.
    /// </summary>
    public IdentifierNode? Identifier { get; }

    /// <summary>
    /// 
    /// </summary>
    public SortDirection Direction { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public SortNode? ThenBy { get; }

    /// <summary>
    /// 
    /// </summary>
    public bool HasThenBy => ThenBy is not null;

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Sort;

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
        if (Identifier is not null)
        {
            foreach (var node1 in Identifier.GetNodesOfType<TNode>())
            {
                yield return node1;
            }
        }
        if (ThenBy is not null)
        {
            foreach (var node1 in ThenBy.GetNodesOfType<TNode>())
            {
                yield return node1;
            }
        }
    }
}
