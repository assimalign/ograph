using System;

namespace Assimalign.OGraph.Syntax;

public sealed class ParameterQueryNode : QueryNode
{
    private QueryNode argument;

    internal ParameterQueryNode() { }
    public ParameterQueryNode(QueryNode argument)
    {
        if (argument is not MemberQueryNode &&
            argument is not ConstantQueryNode && 
            argument is not FunctionQueryNode)
        {
            throw new ArgumentException("");
        }

        this.argument = argument;
    }
    /// <summary>
    /// 
    /// </summary>
    public QueryNode Argument => this.argument;
    /// <summary>
    /// Identifies whether the Argument is a Constant Value (Literal).
    /// </summary>
    public bool IsConstant => Argument is ConstantQueryNode;
    /// <summary>
    /// Identifies whether the Argument is a nested function
    /// </summary>
    public bool IsFunction => Argument is FunctionQueryNode;
    /// <summary>
    /// 
    /// </summary>
    public bool IsMember => Argument is MemberQueryNode;

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Parameter;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }


    internal void SetArgument(QueryNode argument) => this.argument = argument;
}
