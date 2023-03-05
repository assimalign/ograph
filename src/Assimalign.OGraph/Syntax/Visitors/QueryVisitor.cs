using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public abstract class QueryNodeVisitor<T> : IQueryNodeVisitor<T>
{
    private readonly IOGraphNode node;
    private readonly T value;

    public QueryNodeVisitor()
    {
        
    }


    public T Visit(QueryNode node) => node.Accept(this);

    public virtual T Visit(AttributeQueryNode node)
    {
        throw new NotImplementedException();
    }

    public virtual T Visit(RootQueryNode node)
    {
        if (node.TryGetProjections(out var projections))
        {
            foreach (var projection in projections)
            {
                if (projection.IsRoot)
                {

                }
            }
        }

        return value;
        
    }

    public virtual T Visit(FilterQueryNode node)
    {
        throw new NotImplementedException();
    }

    public virtual T Visit(ProjectionQueryNode node)
    {
        throw new NotImplementedException();
    }

    public virtual T Visit(PageQueryNode node)
    {
        throw new NotImplementedException();
    }

    public virtual T Visit(SortQueryNode node)
    {
        throw new NotImplementedException();
    }

    public virtual T Visit(BinaryQueryNode node)
    {
        throw new NotImplementedException();
    }

    public virtual T Visit(PropertyQueryNode node)
    {
        throw new NotImplementedException();
    }

    public virtual T Visit(ParameterQueryNode node)
    {
        throw new NotImplementedException();
    }

    public virtual T Visit(FunctionQueryNode node)
    {
        throw new NotImplementedException();
    }

    public virtual T Visit(ConstantQueryNode node)
    {
        throw new NotImplementedException();
    }
}
