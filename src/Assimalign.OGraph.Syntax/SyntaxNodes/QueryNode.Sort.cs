using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class SortNode : QueryNode
{
    public SortNode() { }

    public SortNode(IdentifierNode identifier)
    {
        this.Identifier = identifier;
    }
    public SortNode(IdentifierNode identifier, SortNode thenBy)
    {
        this.Identifier = identifier;
        this.ThenBy = thenBy;
    }

    /// <summary>
    /// Represents the property or function output to sort by.
    /// </summary>
    public IdentifierNode? Identifier { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public SortDirection Direction { get; init; } = SortDirection.Ascending;
    
    /// <summary>
    /// 
    /// </summary>
    public SortNode? ThenBy { get; init; }

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
