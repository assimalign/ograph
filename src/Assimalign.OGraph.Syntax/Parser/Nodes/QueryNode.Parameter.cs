using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed class ParameterQueryNode : QueryNode
{
    /// <summary>
    /// 
    /// </summary>
    public QueryNode Argument { get; init; }
    /// <summary>
    /// Identifies whether the Argument is a Constant Value (Literal).
    /// </summary>
    public bool IsConstant => Argument is ConstantQueryNode;
    /// <summary>
    /// Identifies whether the Argument is a nested function
    /// </summary>
    public bool IsFunction => Argument is FunctionCallQueryNode;
    /// <summary>
    /// 
    /// </summary>
    public bool IsMember => Argument is MemberQueryNode;

    public override QueryNodeType NodeType => QueryNodeType.Parameter;
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
