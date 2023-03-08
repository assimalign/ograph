using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class FunctionQueryNode : QueryNode
{
    internal FunctionQueryNode() { }
    public FunctionQueryNode(string name, IEnumerable<ParameterQueryNode> parameters)
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
    public IEnumerable<ParameterQueryNode>? Parameters { get; init; } = new ParameterQueryNode[0];

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Function;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}