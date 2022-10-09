using System;
using System.Linq.Expressions;

namespace Assimalign.OGraph.Query;

public sealed partial class ExpressionVisitor
{
    public Expression Visit(BinaryNode node)
    {
        var left = node.Left.Accept(this);
        var right = node.Right.Accept(this);

        return node.BinaryKind switch
        {
            BinaryNodeKind.Equals => Expression.Equal(left, right),
            BinaryNodeKind.NotEquals => Expression.NotEqual(left, right),
            BinaryNodeKind.And => Expression.AndAlso(left, right),
            BinaryNodeKind.Or => Expression.OrElse(left, right),
            BinaryNodeKind.LessThan => Expression.LessThan(left, right),
            BinaryNodeKind.LessThanOrEqual => Expression.LessThanOrEqual(left, right),
            BinaryNodeKind.GreaterThan => Expression.GreaterThan(left, right),
            BinaryNodeKind.GreaterThanOrEqual => Expression.GreaterThanOrEqual(left, right),
            _ => throw new Exception()
        };
    }
}

