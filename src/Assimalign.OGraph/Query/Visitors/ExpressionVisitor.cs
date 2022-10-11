using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Query;

public sealed partial class ExpressionVisitor<T> : IQueryVisitor<Expression>
{
    private readonly Type type;
    private readonly Expression parameterExpression;

    public ExpressionVisitor()
    {
        this.type = typeof(T);
        this.parameterExpression = Expression.Parameter(type);
    }

    internal IQueryable<T> Source { get; init; }

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
        return Call(parameterExpression, node);

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
        var parameter = Expression.Parameter(typeof(T));
        var lambda = Expression.Lambda<Func<T, bool>>(GetLambdaExpressionBody(this, parameter), parameter);
        var where = Expression.Call(
            typeof(Queryable),
            "Where",
            new Type[] { Source.ElementType },
            Source.Expression,
            lambda);
        throw new NotImplementedException();
    }

    public Expression Visit(SelectNode node)
    {
        throw new NotImplementedException();
    }

    public Expression Visit(SortNode node)
    {
        var method = node.SortKind == SortNodeKind.Ascending ? "" : node.SortKind.ToString();
        // Need to create a new parameter expression, separate from the one above, or will get error 
        // This is due to the one above being used for deep nested filters
        var parameterExpression = Expression.Parameter(type);
        var memebrExpression = node.Member.Accept(this);
        var lambdaExpresion = Expression.Lambda<Func<T, object>>(
            memebrExpression.Type.IsValueType == true ? Expression.Convert(memebrExpression, typeof(object)) : memebrExpression,
            parameterExpression);

        var callExpression = Expression.Call(
            typeof(Queryable),
            $"OrderBy{method}",
            new Type[] { Source.ElementType, typeof(object) },
            Source.Expression,
            lambdaExpresion);

        if (node.ThenBy is not null)
        {
            node.ThenBy.Accept(this);
        }

        return callExpression;
    }
}
