using System;
using System.Security.Claims;
using Assimalign.OGraph.Syntax;

namespace Assimalign.OGraph.Internal;

internal class OGraphResolverContext :
    IOGraphPropertyContext,
    IOGraphOperationContext
{
    public IServiceProvider? ServiceProvider { get; init; }
    public IOGraphExecutorRequest Request { get; init; }
    public IOGraphExecutorResponse Response { get; init; }

    public ClaimsPrincipal GetClaimsPrincipal()
    {
        throw new NotImplementedException();
    }

    public IOGraphEdge GetEdge()
    {
        throw new NotImplementedException();
    }

    public IOGraphType GetEdgeSource()
    {
        throw new NotImplementedException();
    }

    public IOGraphType GetEdgeTarget()
    {
        throw new NotImplementedException();
    }

    public IOGraph GetGraph()
    {
        throw new NotImplementedException();
    }

    public IOGraphOperation GetOperation()
    {
        throw new NotImplementedException();
    }

    public T GetParent<T>()
    {
        throw new NotImplementedException();
    }

    public IOGraphType GetPropertyType()
    {
        throw new NotImplementedException();
    }

    public QueryDocument GetQuery()
    {
        throw new NotImplementedException();
    }

    public OGraphQueryOptions GetQueryOptions()
    {
        throw new NotImplementedException();
    }

    public IOGraphQueryProvider GetQueryProvider()
    {
        throw new NotImplementedException();
    }

    public T? GetService<T>()
    {
        throw new NotImplementedException();
    }
}
