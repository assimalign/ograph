using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Syntax;

internal class OGraphOperationContext : IOGraphOperationContext
{
    public IOGraph Graph { get; init; }
    public IOGraphOperation Operation { get; init; }
    public QueryDocument Query { get; init; }

    public IServiceProvider ServiceProvider { get; init; }

    public ClaimsPrincipal GetClaimsPrincipal()
    {
        return ClaimsPrincipal.Current;
    }

    public IOGraphEdge GetEdge(Name name)
    {
        throw new NotImplementedException();
    }

    public IOGraph GetGraph()
    {
        return Graph;
    }

    public IOGraphNode GetNode()
    {
        return Operation.Node;
    }

    public QueryDocument GetQuery()
    {
        return Query;
    }

    public OGraphQueryOptions GetQueryOptions()
    {
        return Operation.QueryOptions;
    }

    public IOGraphQueryProvider GetQueryProvider()
    {
        return Operation.QueryProvider;
    }

    public T GetRequestBody<T>()
    {
        throw new NotImplementedException();
    }

    public Stream GetRequestBody()
    {
        throw new NotImplementedException();
    }

    public T GetRequestHeader<T>(string headerName)
    {
        throw new NotImplementedException();
    }

    public T GetRequestQueryValue<T>(string parameterName)
    {
        throw new NotImplementedException();
    }

    public T GetRequestRouteParam<T>(string parameterName)
    {
        throw new NotImplementedException();
    }

    public Stream GetResponseBody()
    {
        throw new NotImplementedException();
    }

    public T GetService<T>()
    {
        var service =  ServiceProvider.GetService(typeof(T));

        if (service is null)
        {
            throw new InvalidOperationException();
        }
        return (T)service;
    }
}
