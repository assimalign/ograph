
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Execution;

internal class OGraphResolverContext :
    IOGraphEdgeResolverContext,
    IOGraphPropertyResolverContext,
    IOGraphOperationResolverContext
{

    public OGraphResolverContext()
    {
        
    }


    internal volatile object Parent;


    internal IServiceProvider ServiceProvider { get; init; }
    internal IOGraphHeaderCollection Headers { get; init; }






    public T GetBodyValue<T>()
    {
        throw new NotImplementedException();
    }

    public ClaimsPrincipal GetClaimsPrincipal()
    {
        throw new NotImplementedException();
    }

    public T GetHeaderValue<T>(string headerName)
    {
        throw new NotImplementedException();
    }

    public T GetParent<T>()
    {
        if (Parent is T instance)
        {
            return instance;
        }
        throw new InvalidOperationException("");
    }

    public T GetQueryValue<T>(string parameterName)
    {
        throw new NotImplementedException();
    }

    public T GetRouteValue<T>(string parameterName)
    {
        throw new NotImplementedException();
    }

    public T GetService<T>()
    {
        throw new NotImplementedException();
    }
}
