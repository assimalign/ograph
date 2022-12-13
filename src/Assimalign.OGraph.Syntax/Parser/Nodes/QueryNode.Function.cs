using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class FunctionQueryNode : QueryNode
{

    public FunctionQueryNode() { }
    public FunctionQueryNode(string name, IEnumerable<ParameterQueryNode> parameters)
    {
        this.Name = name;
        this.Parameters = parameters;
    }


    /// <summary>
    /// The function parameters
    /// </summary>
    public IEnumerable<ParameterQueryNode> Parameters { get; init; }
    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Function;
    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
