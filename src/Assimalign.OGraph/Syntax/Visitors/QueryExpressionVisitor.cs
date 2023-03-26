using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public sealed class QueryExpressionVisitor : IQueryNodeVisitor<Expression>
{
    private readonly Expression expression;

    public QueryExpressionVisitor(Expression expression)
    {
        this.expression = expression;
    }

    public Expression Visit(QueryNode queryNode)
    {
        return expression;
    }

    public Expression Visit(RootNode queryNode)
    {
        return expression;
    }

    public Expression Visit(FilterNode queryNode)
    {
        throw new NotImplementedException();
    }

    public Expression Visit(ProjectionNode queryNode)
    {
        throw new NotImplementedException();
    }

    public Expression Visit(PageNode queryNode)
    {
        throw new NotImplementedException();
    }

    public Expression Visit(SortNode queryNode)
    {
        throw new NotImplementedException();
    }

    public Expression Visit(BinaryNode queryNode)
    {
        throw new NotImplementedException();
    }

    public Expression Visit(PropertyNode queryNode)
    {
        throw new NotImplementedException();
    }

    public Expression Visit(ParameterNode queryNode)
    {
        throw new NotImplementedException();
    }

    public Expression Visit(FunctionCallNode queryNode)
    {
        throw new NotImplementedException();
    }

    public Expression Visit(ConstantNode queryNode)
    {
        throw new NotImplementedException();
    }

    public Expression Visit(EdgeNode queryNode)
    {
        throw new NotImplementedException();
    }
}
