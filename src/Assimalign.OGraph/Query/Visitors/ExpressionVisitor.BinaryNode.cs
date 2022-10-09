using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
        };
    }
}

