using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed class QueryableQueryVisitor<T> : IQueryNodeVisitor<IQueryable<T>>
{
    private IQueryable<T> queryable;

    public QueryableQueryVisitor(IQueryable<T> queryable)
    {
        this.queryable = queryable;
    }

    public IQueryable<T> Visit(QueryNode queryNode)
    {
        return queryNode.Accept(this);
    }

    public IQueryable<T> Visit(VertexNode queryNode)
    {
        foreach (var node in queryNode.Nodes)
        {
           queryable = node.Accept(this);
        }

        return queryable;
    }

    public IQueryable<T> Visit(FilterNode queryNode)
    {
        return queryable;
    }

    public IQueryable<T> Visit(ProjectNode queryNode)
    {
        return queryable;
    }

    public IQueryable<T> Visit(PageNode queryNode)
    {
        var skip = queryNode.Skip.GetInt32();
        var take = queryNode.Take.GetInt32();

        var skipExpression = Expression.Call(
            typeof(Queryable),
            "Skip",
            new Type[] { queryable.ElementType },
            queryable.Expression,
            Expression.Constant(skip));

        var takeExpression = Expression.Call(
            typeof(Queryable),
            "Take",
            new Type[] { queryable.ElementType },
            skipExpression,
            Expression.Constant(take));

        queryable = queryable.Provider.CreateQuery<T>(takeExpression);

        return queryable;
    }

    public IQueryable<T> Visit(SortNode queryNode)
    {
        return queryable;
    }

    public IQueryable<T> Visit(BinaryNode queryNode)
    {
        return queryable;
    }

    public IQueryable<T> Visit(PropertyNode queryNode)
    {
        return queryable;
    }

    public IQueryable<T> Visit(ParameterNode queryNode)
    {
        return queryable;
    }

    public IQueryable<T> Visit(FunctionCallNode queryNode)
    {
        return queryable;
    }

    public IQueryable<T> Visit(ConstantNode queryNode)
    {
        return queryable;
    }

    public IQueryable<T> Visit(EdgeNode queryNode)
    {
        return queryable;
    }
}
