using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class FunctionQueryNode : QueryNode
{
    public FunctionQueryNode() { }
    public FunctionQueryNode(string name, IEnumerable<ParameterNode> parameters)
    {
        this.Name = name;
        this.Parameters = parameters;
    }

    /// <summary>
    /// The name of the function call.
    /// </summary>
    public string? Name { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public FunctionType FunctionType { get; init; }
    /// <summary>
    /// The function parameters
    /// </summary>
    public IEnumerable<ParameterNode> Parameters { get; init; } = new ParameterNode[0];

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Function;

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
        foreach (var parameter in Parameters)
        {
            foreach (var item in parameter.GetNodesOfType<TNode>())
            {
                yield return item;
            }
        }
    }
}