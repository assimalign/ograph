using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
[DebuggerDisplay("{Name}")]
public sealed class PropertyNode : IdentifierNode
{
    internal PropertyNode(string name, string text, Location location)
        : base(name, text, location)
    {
        Children = [];
    }
    internal PropertyNode(string name, string alias, string text, Location location) 
        : this(name, text, location)
    {
        Alias = alias;
    }
    internal PropertyNode(string name, IEnumerable<PropertyNode> children, string text, Location location)
        : this(name, text, location)
    {
        Children = children.ToImmutableList();
    }
    internal PropertyNode(string name, string alias, IEnumerable<PropertyNode> children, string text, Location location) 
        : this(name, alias, text, location)
    {
        Children = children.ToImmutableList();
    }

    /// <summary>
    /// A temporary name to be assigned in replacement of the property name.
    /// </summary>
    public string? Alias { get; }

    /// <summary>
    /// Represents nested Identifiers
    /// </summary>
    public IEnumerable<PropertyNode> Children { get; }

    /// <summary>
    /// Specifies whether the Attribute has nested Identifiers.
    /// </summary>
    public bool HasChildren => Children.Any();

    /// <summary>
    /// Specifies whether there is an alias.
    /// </summary>
    public bool HasAlias => !string.IsNullOrEmpty(Alias);

    #region Overloads
    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Property;

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
        if (Children is not null)
        {
            foreach (var node1 in Children.SelectMany(x => x.GetNodesOfType<TNode>()))
            {
                yield return node1;
            }
        }
    }
    #endregion

}
