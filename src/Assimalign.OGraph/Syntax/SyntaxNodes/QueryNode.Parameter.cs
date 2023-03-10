using System;

namespace Assimalign.OGraph.Syntax;

public sealed class ParameterQueryNode : QueryNode
{
    private QueryNode? parameterValue;

    internal ParameterQueryNode() { }
    public ParameterQueryNode(PropertyQueryNode parameterValue)
    {
        this.parameterValue = parameterValue;
    }
    public ParameterQueryNode(ConstantQueryNode parameterValue)
    {
        this.parameterValue = parameterValue;
    }
    public ParameterQueryNode(FunctionQueryNode parameterValue)
    {
        this.parameterValue = parameterValue;
    }

    /// <summary>
    /// 
    /// </summary>
    public QueryNode? ParameterValue { get; init; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="constant"></param>
    /// <returns></returns>
    public bool IsConstant(out ConstantQueryNode? constant)
    {
        constant = default;

        if (constant is ConstantQueryNode node)
        {
            constant = node;
            return true;
        }

        return false;
    }



    /// <summary>
    /// 
    /// </summary>
    public ParameterType ParameterType => this.parameterValue switch
    {
        ConstantQueryNode => ParameterType.Constant,
        FunctionQueryNode => ParameterType.Function,
        PropertyQueryNode => ParameterType.Property,
        _                 => ParameterType.None
    };

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Parameter;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
