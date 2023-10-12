using System;
using System.Linq;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;
using System.Threading.Tasks;
using System.Threading;

public abstract class OGraphCommandOperation : IOGraphCommandOperation
{
    private int chainIndex;

    internal Name name;
    internal Route route;
    internal Method method;
    internal IOGraphNode node;
    internal OGraphQueryOptions queryOptions;
    internal IOGraphQueryProvider queryProvider;
    internal IOGraphOperationResolver resolver;

    public OGraphCommandOperation()
    {
        this.Metadata = new OGraphMetadata();
        this.Middleware = new OGraphOperationMiddlewareQueue();
        this.queryOptions = OGraphQueryOptions.Default;

        Configure(new OGraphCommandOperationDescriptor(this));
    }

    public Name Name => this.name;
    public Route Route => this.route;
    public Method Method => this.method;
    public IOGraphNode Node => this.node;
    public IOGraphOperationResolver Resolver => this.resolver;
    public IOGraphOperationMiddlewareQueue Middleware { get; }
    public IOGraphMetadata Metadata { get; }
    public OperationType OperationType => OperationType.Command;

    public bool IsEnabled => throw new NotImplementedException();

    public OGraphOperationHandler BuildHandlerChain()
    {
        var memoise = Cacher<OGraphCommandOperation, OGraphOperationHandler>.Memoise(operation =>
        {
            if (operation.Resolver is null)
            {
                throw new InvalidOperationException("No resolver was added. Handler Chain requires a resolver.");
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

   
    protected virtual void Configure(IOGraphCommandOperationDescriptor descriptor) { }

    private OGraphOperationHandler GetResolverChain(OGraphOperationHandler handler)
    {
        var middleware = Middleware.Reverse().Skip(chainIndex).First();
        var next = new OGraphOperationHandler((context, cancellationToken) =>
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


    Task<IOGraphResult> IOGraphOperation.ExecuteAsync(IOGraphOperationContext context, CancellationToken cancellationToken)
    {
        return Middleware.BuildHandlerChain(Resolver).Invoke(context, cancellationToken);
    }
}