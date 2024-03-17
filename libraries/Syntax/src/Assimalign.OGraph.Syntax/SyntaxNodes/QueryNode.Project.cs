using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// Unlike select statements, projections are not limited to a single entity.
/// </remarks>
public sealed class ProjectNode : QueryNode
{
    /// <summary>
    /// A default constructor for <see cref="ProjectNode"/>.
    /// </summary>
    /// <param name="properties"></param>
    public ProjectNode(IEnumerable<PropertyNode> properties, string text, Location location) 
        : base(text, location)
    {
        if (properties is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(properties));
        }
        Properties = properties.ToImmutableList();
    }

    /// <summary>
    /// A collection of properties to project in the query results.
    /// </summary>
    public IEnumerable<PropertyNode> Properties { get; }

    #region Overloads

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Project;

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
        if (this is TNode project)
        {
            yield return project;
        }
        foreach (var property in Properties)
        {
            foreach (var node in property.GetNodesOfType<TNode>())
            {
                yield return node;
            }
        }
    }
    #endregion
}
