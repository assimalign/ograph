using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class FunctionCallNode : IdentifierNode
{
    public FunctionCallNode(string name, IEnumerable<ParameterNode> parameters) : base(name)
    {
        this.Parameters = parameters;
    }

    /// <summary>
    /// 
    /// </summary>
    public FunctionType FunctionType { get; init; }
    /// <summary>
    /// The function parameters
    /// </summary>
    public IEnumerable<ParameterNode> Parameters { get; init; } = [];

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.FunctionCall;

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
        foreach (var parameter in Parameters)
        {
            foreach (var item in parameter.GetNodesOfType<TNode>())
            {
                yield return item;
            }
        }
    }
}