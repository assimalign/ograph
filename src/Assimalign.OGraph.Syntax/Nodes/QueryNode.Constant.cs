using System;

namespace Assimalign.OGraph.Syntax;

public sealed class ConstantQueryNode : QueryNode
{
    private object value;

    /// <summary>
    /// 
    /// </summary>
    public object Value
    {
        get => this.value;
        init
        {
            this.value = value;
        }
    }

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Constant;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
