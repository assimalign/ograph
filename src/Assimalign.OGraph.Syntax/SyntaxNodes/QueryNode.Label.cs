using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class LabelNode : IdentifierNode
{
    public LabelNode(string label)
    {
        Name = label;
    }

    /// <summary>
    /// A temporary name to be assigned in replacement of the property name.
    /// </summary>
    public string? Alias { get; init; }

    /// <summary>
    /// Check whether an alias is available.
    /// </summary>
    public bool HasAlias => !string.IsNullOrEmpty(Alias);

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Label;

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
    }
}
