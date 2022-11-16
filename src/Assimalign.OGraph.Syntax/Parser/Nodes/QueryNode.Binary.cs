using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed class BinaryQueryNode : QueryNode
{
    public QueryNode Left { get; init; }
    public QueryNode Right { get; init; }
    public BinaryOperatorType OperatorType { get; init; }
    public bool IsPredeicate
    {
        get 
        {
            return OperatorType == BinaryOperatorType.Equal || 
                   OperatorType == BinaryOperatorType.NotEqual || 
                   OperatorType == BinaryOperatorType.LessThan ||
                   OperatorType == BinaryOperatorType.LessThanOrEqual ||
                   OperatorType == BinaryOperatorType.GreaterThan || 
                   OperatorType == BinaryOperatorType.GreaterThanOrEqual ||
                   OperatorType == BinaryOperatorType.And ||
                   OperatorType == BinaryOperatorType.Or || 
                   OperatorType == BinaryOperatorType.Any;    
        }
    }

    public override QueryNodeType NodeType => QueryNodeType.Binary;

    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
