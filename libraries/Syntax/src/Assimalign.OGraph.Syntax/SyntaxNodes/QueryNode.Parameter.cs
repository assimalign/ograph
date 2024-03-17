using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

using Assimalign.OGraph.Syntax.Internal;

public sealed class ParameterNode : QueryNode
{
    ParameterNode(QueryNode parameter)
    {
        if (parameter is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(parameter));
        }
        ParameterValue = parameter;
    }
    public ParameterNode(PropertyNode parameter) 
        : this(parameter as QueryNode) { }
    public ParameterNode(ConstantNode parameter)
        : this(parameter as QueryNode) { }
    public ParameterNode(FunctionCallNode parameter)
        : this(parameter as QueryNode) { }

    /// <summary>
    /// 
    /// </summary>
    public QueryNode? ParameterValue { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public ParameterType ParameterType => ParameterValue switch
    {
        ConstantNode        => ParameterType.Constant,
        FunctionCallNode    => ParameterType.Function,
        PropertyNode        => ParameterType.Property,
        _                   => ParameterType.None
    };

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Parameter;

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
        if (ParameterValue is not null)
        {
            foreach (var node1 in ParameterValue.GetNodesOfType<TNode>())
            {
                yield return node1;
            }
        }
    }
}
