using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class PropertyNode : IdentifierNode
{
    internal PropertyNode() { }
    /// <summary>
    /// A default constructor for <see cref="PropertyNode"/>.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    public PropertyNode(string name)
    {
        base.Name = name;
    }

    /// <summary>
    /// A default constructor for <see cref="PropertyNode"/>.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="alias">The alias to use in place of the property name.</param>
    public PropertyNode(string name, string alias) 
        : this(name)
    {
        this.Alias = alias;
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
        this.Children = children;
    }

    /// <summary>
    /// A temporary name to be assigned in replacement of the property name.
    /// </summary>
    public string? Alias { get; init; }

    /// <summary>
    /// Represents nested Identifiers
    /// </summary>
    public IEnumerable<PropertyNode> Children { get; init; } = new PropertyNode[0];

    /// <summary>
    /// Specifies whether the Attribute has nested Identifiers.
    /// </summary>
    public bool HasChildren => Children is not null && Children.Any();

    /// <summary>
    /// Specifies whether there is an alias.
    /// </summary>
    public bool HasAlias => !string.IsNullOrEmpty(Alias);

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Property;

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
}
