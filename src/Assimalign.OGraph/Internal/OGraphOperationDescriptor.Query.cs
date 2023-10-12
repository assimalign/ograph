using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphQueryOperationDescriptor : IOGraphQueryOperationDescriptor
{
    private readonly OGraphQueryOperation operation;

    public OGraphQueryOperationDescriptor(OGraphQueryOperation operation)
    {
        this.operation = operation;
    }

    public IList<Action<OGraph>> OnConfigure { get; init; }
    public IOGraphQueryOperationDescriptor UseMiddleware<TMiddleware>() where TMiddleware : IOGraphOperationMiddleware, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphQueryOperationDescriptor UseMiddleware(IOGraphOperationMiddleware middleware)
    {
        throw new NotImplementedException();
    }

    public IOGraphQueryOperationDescriptor UseMiddleware(OGraphOperationMiddleware middleware)
    {
        throw new NotImplementedException();
    }

    public IOGraphQueryOperationDescriptor UseName(Name name)
    {
        throw new NotImplementedException();
    }

    public IOGraphQueryOperationDescriptor UseNode(Name name)
    {
        throw new NotImplementedException();
    }

    public IOGraphQueryOperationDescriptor UseNode<TNode>() where TNode : IOGraphNode, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphQueryOperationDescriptor UseQueryOptions(OGraphQueryOptions options)
    {
        throw new NotImplementedException();
    }

    public IOGraphQueryOperationDescriptor UseQueryOptions<TQueryOptions>(Action<TQueryOptions> configure) where TQueryOptions : OGraphQueryOptions, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphQueryOperationDescriptor UseQueryOptions(Action<OGraphQueryOptions> configure)
    {
        throw new NotImplementedException();
    }

    public IOGraphQueryOperationDescriptor UseQueryParam(string paramKey)
    {
        throw new NotImplementedException();
    }

    public IOGraphQueryOperationDescriptor UseQueryProvider<TQueryProvider>() where TQueryProvider : IOGraphQueryProvider, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphQueryOperationDescriptor UseQueryProvider(IOGraphQueryProvider queryProvider)
    {
        throw new NotImplementedException();
    }

    public IOGraphQueryOperationDescriptor UseResolver<TResolver>() where TResolver : IOGraphOperationResolver, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphQueryOperationDescriptor UseResolver(IOGraphOperationResolver resolver)
    {
        throw new NotImplementedException();
    }

    public IOGraphQueryOperationDescriptor UseResolver(OGraphOperationResolver resolver)
    {
        throw new NotImplementedException();
    }

    public IOGraphQueryOperationDescriptor UseRoute(Route route)
    {
        throw new NotImplementedException();
    }
}
