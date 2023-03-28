using System;
using System.Linq;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;

public abstract class OGraphOperation : IOGraphOperation
{
    private int chainIndex;

    internal Name name;
    internal Route route;
    internal Method method;
    internal IOGraphNode node;
    internal OGraphQueryOptions queryOptions;
    internal IOGraphQueryProvider queryProvider;
    internal IOGraphOperationResolver resolver;

    public OGraphOperation()
    {
        this.Metadata = new OGraphMetadata();
        this.Middleware = new OGraphOperationMiddlewareQueue();
        this.queryOptions = new OGraphQueryOptionsDefault();

        Configure(new OGraphOperationDescriptor(this));
    }

    /// <inheritdoc />
    public Name Name => this.name;

    /// <inheritdoc />
    public Route Route => this.route;

    /// <inheritdoc />
    public Method Method => this.method;

    /// <inheritdoc />
    public bool IsEnabled => throw new NotImplementedException();

    /// <inheritdoc />
    public IOGraphNode Node => this.node;

    /// <inheritdoc />
    public IOGraphOperationResolver Resolver => this.resolver;

    /// <inheritdoc />
    public OGraphQueryOptions QueryOptions => this.queryOptions;

    /// <inheritdoc />
    public IOGraphQueryProvider QueryProvider => this.queryProvider;

    /// <inheritdoc />
    public IOGraphOperationMiddlewareQueue Middleware { get; }

    /// <inheritdoc />
    public IOGraphMetadata Metadata { get; }

    /// <inheritdoc />
    public IOGraphType RequestType => throw new NotImplementedException();

    /// <inheritdoc />
    public IOGraphType ResponseType => throw new NotImplementedException();

    /// <inheritdoc />
    public OGraphOperationHandler GetResolverChain()
    {
        var memoise = Cacher<OGraphOperation, OGraphOperationHandler>.Memoise(operation =>
        {
            if (operation.Resolver is null)
            {
                throw new Exception();
            }
            var root = new OGraphOperationHandler(operation.Resolver.InvokeAsync);

            if (operation.Middleware.Count == 0)
            {
                return root;
            }
            return GetResolverChain(root);
        });

        return memoise.Invoke(this);
    }
    

    protected virtual void Configure(IOGraphOperationDescriptor descriptor) { }

    private OGraphOperationHandler GetResolverChain(OGraphOperationHandler handler)
    {
        var middleware = Middleware.Reverse().Skip(chainIndex).First();
        var next = new OGraphOperationHandler(context =>
        {
            return middleware.InvokeAsync(context, handler);
        });
        if (chainIndex < Middleware.Count - 1)
        {
            chainIndex++;
            return GetResolverChain(next);
        }
        chainIndex = 0;
        return next;
    }
}