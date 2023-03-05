using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed class OGraphQueryVisitor<T> : IQueryNodeVisitor<Expression>
{
    private static readonly Type type = typeof(T);

    private IQueryable<T> queryable;


    public Expression Visit(QueryNode node)
    {
        throw new NotImplementedException();
    }
    public Expression Visit(RootQueryNode node)
    {
        throw new NotImplementedException();
    }
    public Expression Visit(FilterQueryNode node)
    {
        throw new NotImplementedException();
    }
    public Expression Visit(ProjectionQueryNode node)
    {
        throw new NotImplementedException();
    }
    public Expression Visit(PageQueryNode node)
    {
        var offset = Expression.Call(
            typeof(Queryable),
            "Skip",
            new Type[]
            { 
                queryable.ElementType 
            },
            queryable.Expression,
            Expression.Constant(node.Skip.Value));

        return (queryable = (IQueryable<T>)queryable.Provider.CreateQuery(offset)).Expression;
    }
    public Expression Visit(SortQueryNode node)
    {
        foreach (var field in node.Attributes)
        {
            
        }
        throw new NotImplementedException();
    }
    public Expression Visit(BinaryQueryNode node)
    {
        throw new NotImplementedException();
    }
    public Expression Visit(AttributeQueryNode node)
    {
        throw new NotImplementedException();
    }
    public Expression Visit(PropertyQueryNode node)
    {
        throw new NotImplementedException();
    }
    public Expression Visit(ParameterQueryNode node)
    {
        throw new NotImplementedException();
    }
    public Expression Visit(FunctionQueryNode node)
    {
        throw new NotImplementedException();
    }
    public Expression Visit(ConstantQueryNode node)
    {
        throw new NotImplementedException();
    }
}
