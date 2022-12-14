using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class FunctionQueryNode : QueryNode
{
    private readonly List<ParameterQueryNode> parameters = new();

    ~FunctionQueryNode() { }
    public FunctionQueryNode(string name, IEnumerable<ParameterQueryNode> parameters)
    {
        this.Name = name;
        this.parameters.AddRange(parameters);
    }

    /// <summary>
    /// The name of the function call.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The function parameters
    /// </summary>
    public IEnumerable<ParameterQueryNode> Parameters => this.parameters;

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Function;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}