using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public sealed class SortNode : QueryNode
{
    internal SortDirection direction = SortDirection.Ascending;
    internal IdentifierNode? identifier;
    internal SortNode? thenBy;

    SortNode() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="identifier"></param>
    /// <exception cref="ArgumentNullException"/>
    public SortNode(IdentifierNode identifier)
    {
        if (identifier is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(identifier));
        }
        this.identifier = identifier;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="direction"></param>
    /// <exception cref="ArgumentNullException"/>
    public SortNode(IdentifierNode identifier, SortDirection direction) : this(identifier)
    {
        this.direction = direction;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="direction"></param>
    /// <param name="thenBy"></param>
    /// <exception cref="ArgumentNullException"/>
    public SortNode(IdentifierNode identifier, SortDirection direction, SortNode thenBy) : this(identifier, direction)
    {
        if (thenBy is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(thenBy));
        }
        this.thenBy = thenBy;
    }

    /// <summary>
    /// Represents the property or function output to sort by.
    /// </summary>
    public IdentifierNode? Identifier => this.identifier;

    /// <summary>
    /// 
    /// </summary>
    public SortDirection Direction => direction;
    
    /// <summary>
    /// 
    /// </summary>
    public SortNode? ThenBy => thenBy;

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


    internal static SortNode Create() => new SortNode();
}
