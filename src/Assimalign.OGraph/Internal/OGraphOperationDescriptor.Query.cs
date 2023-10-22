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

    public IList<Action<Graph>> OnConfigure { get; init; }
    public IOGraphQueryOperationDescriptor UseMiddleware<TMiddleware>() where TMiddleware : IOGraphOperationMiddleware, new()
    {
        return this;
    }

    public IOGraphQueryOperationDescriptor UseMiddleware(IOGraphOperationMiddleware middleware)
    {
        return this;
    }

    public IOGraphQueryOperationDescriptor UseMiddleware(OGraphOperationMiddleware middleware)
    {
        return this;
    }

    public IOGraphQueryOperationDescriptor UseName(Name name)
    {
        operation.name = name;
        return this;
    }

    public IOGraphQueryOperationDescriptor UseNode(Name name)
    {
        return this;
    }

    public IOGraphQueryOperationDescriptor UseNode<TNode>() where TNode : IOGraphVertex, new()
    {
        return this;
    }

    public IOGraphQueryOperationDescriptor UseQueryOptions(OGraphQueryOptions options)
    {
        return this;
    }

    public IOGraphQueryOperationDescriptor UseQueryOptions<TQueryOptions>(Action<TQueryOptions> configure) where TQueryOptions : OGraphQueryOptions, new()
    {
        return this;
    }

    public IOGraphQueryOperationDescriptor UseQueryOptions(Action<OGraphQueryOptions> configure)
    {
        return this;
    }

    public IOGraphQueryOperationDescriptor UseQueryParam(string paramKey)
    {
        return this;
    }

    public IOGraphQueryOperationDescriptor UseQueryProvider<TQueryProvider>() where TQueryProvider : IOGraphQueryProvider, new()
    {
        return this;
    }

    public IOGraphQueryOperationDescriptor UseQueryProvider(IOGraphQueryProvider queryProvider)
    {
        return this;
    }

    public IOGraphQueryOperationDescriptor UseResolver<TResolver>() where TResolver : IOGraphOperationResolver, new()
    {
        return this;
    }

    public IOGraphQueryOperationDescriptor UseResolver(IOGraphOperationResolver resolver)
    {
        return this;
    }

    public IOGraphQueryOperationDescriptor UseResolver(OGraphOperationResolver resolver)
    {
        return this;
    }

    public IOGraphQueryOperationDescriptor UseRoute(Route route)
    {
        operation.route = route;
        return this;
    }
}
