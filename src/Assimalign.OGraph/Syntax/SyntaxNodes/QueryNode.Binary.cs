using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assimalign.OGraph.Syntax;

public sealed class BinaryQueryNode : QueryNode
{


    internal BinaryQueryNode() { }
    public BinaryQueryNode(QueryNode left, QueryNode right, BinaryOperatorType operatorType)
    {
        LeftOperand = left;
        RightOperand = right;
        OperatorType = operatorType;
    }


    /// <summary>
    /// 
    /// </summary>
    public QueryNode? LeftOperand { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public QueryNode? RightOperand { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public BinaryOperatorType OperatorType { get; init; }

    //public bool IsPredeicate
    //{
    //    get 
    //    {
    //        return OperatorType == BinaryOperatorType.Equal || 
    //               OperatorType == BinaryOperatorType.NotEqual || 
    //               OperatorType == BinaryOperatorType.LessThan ||
    //               OperatorType == BinaryOperatorType.LessThanOrEqual ||
    //               OperatorType == BinaryOperatorType.GreaterThan || 
    //               OperatorType == BinaryOperatorType.GreaterThanOrEqual ||
    //               OperatorType == BinaryOperatorType.And ||
    //               OperatorType == BinaryOperatorType.Or || 
    //               OperatorType == BinaryOperatorType.Any;    
    //    }
    //}

    /// <inheritdoc />
    public override QueryNodeType NodeType => QueryNodeType.Binary;

    /// <inheritdoc />
    public override T Accept<T>(IQueryNodeVisitor<T> visitor)
    {
        return visitor.Visit(this);
    }
}
