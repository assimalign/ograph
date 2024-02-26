using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Assimalign.OGraph.Syntax;

/// <summary>
/// 
/// </summary>
[DebuggerDisplay("{Name}")]
public sealed class PropertyNode : IdentifierNode
{

    /// <summary>
    /// A default constructor for <see cref="PropertyNode"/>.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    public PropertyNode(string name)
        : base(name)
    {
        Children = [];
    }

    /// <summary>
    /// A default constructor for <see cref="PropertyNode"/>.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="alias">The alias to use in place of the property name.</param>
    public PropertyNode(string name, string alias) 
        : this(name)
    {
        Alias = alias;
    }

    public PropertyNode(string name, IEnumerable<PropertyNode> children)
        : this(name)
    {
        Children = children.ToImmutableList();
    }

    /// <summary>
    /// A default constructor for <see cref="PropertyNode"/>.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="alias">The alias to use in place of the property name.</param>
    /// <param name="children">A collection of nested properties.</param>
    public PropertyNode(string name, string alias, IEnumerable<PropertyNode> children) 
        : this(name, alias)
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
