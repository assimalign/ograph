using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Query;

public sealed partial class ExpressionVisitor : IQueryVisitor<Expression>
{
    public readonly ParameterExpression parameter;

    public ExpressionVisitor()
    {

    }


    public Expression Visit(QueryNode node)
    {
        return node switch
        {
            RootNode rn => Visit(rn),
            BinaryNode bn => Visit(bn),
            ConstantNode cn => Visit(cn),
            SortNode sn => Visit(sn),
            FilterNode fn => Visit(fn),
            SelectNode sln => Visit(sln),

            _ => throw new Exception()
        };
    }

    public Expression Visit(RootNode node)
    {
        throw new NotImplementedException();
    }

    public Expression Visit(MemberNode node)
    {
        return Call(parameter, node);

        Expression Call(Expression exp, MemberNode node)
        {
            var member = Expression.PropertyOrField(exp, node.Name);
            if (node.Child is not null)
            {
                return Call(member, node.Child);
            }
            return member;
        }
    }

    public Expression Visit(FilterNode node)
    {
        throw new NotImplementedException();
    }

    public Expression Visit(SelectNode node)
    {
        throw new NotImplementedException();
    }

    public Expression Visit(SortNode node)
    {
        throw new NotImplementedException();
    }
}
