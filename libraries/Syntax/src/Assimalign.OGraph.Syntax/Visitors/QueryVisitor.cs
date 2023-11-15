using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax;

public abstract class QueryVisitor<T> : IQueryNodeVisitor<T>
{
    
    public T Visit(QueryNode node) => node.Accept(this);


    public virtual T Visit(VertexNode node)
    {
        throw new NotImplementedException();
    }

    public virtual T Visit(FilterNode node)
    {
        throw new NotImplementedException();
    }

    public virtual T Visit(ProjectionNode node)
    {
        throw new NotImplementedException();
    }

    public virtual T Visit(PageNode node)
    {
        throw new NotImplementedException();
    }

    public virtual T Visit(SortNode node)
    {
        throw new NotImplementedException();
    }

    public virtual T Visit(BinaryNode node)
    {
        throw new NotImplementedException();
    }

    public virtual T Visit(PropertyNode node)
    {
        throw new NotImplementedException();
    }

    public virtual T Visit(ParameterNode node)
    {
        throw new NotImplementedException();
    }

    public virtual T Visit(FunctionCallNode node)
    {
        throw new NotImplementedException();
    }

    public virtual T Visit(ConstantNode node)
    {
        throw new NotImplementedException();
    }

    public T Visit(EdgeNode queryNode)
    {
        throw new NotImplementedException();
    }
}
