using System.Collections.Generic;
using System.Diagnostics;

namespace Assimalign.OGraph.Syntax;

[DebuggerDisplay("{Name}")]
public sealed class LabelNode : IdentifierNode
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    public LabelNode(string label) : base(label) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <param name="alias"></param>
    public LabelNode(string label, string alias) : this(label)
    {
        Alias = alias;
    }

    /// <summary>
    /// A temporary name to be assigned in replacement of the property name.
    /// </summary>
    public string? Alias { get; }

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

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static LabelNode Empty() => new LabelNode(string.Empty);
}
