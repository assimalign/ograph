using Assimalign.OGraph.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public abstract class OGraphQueryOperation : IOGraphQueryOperation
{
    private int chainIndex;

    internal Name name;
    internal Route route;
    internal Method method;
    internal IOGraphNode node;
    internal OGraphQueryOptions queryOptions;
    internal IOGraphQueryProvider queryProvider;
    internal IOGraphOperationResolver resolver;

    public OGraphQueryOperation()
    {
        this.Metadata = new OGraphMetadata();
        this.Middleware = new OGraphOperationMiddlewareQueue();
        this.queryOptions = OGraphQueryOptions.Default;

        Configure(new OGraphQueryOperationDescriptor(this));
    }

    public Name Name => this.name;
    public Route Route => this.route;
    public Method Method => this.method;
    public IOGraphNode Node => this.node;
    public IOGraphOperationResolver Resolver => this.resolver;
    public IOGraphOperationMiddlewareQueue Middleware { get; }
    public IOGraphMetadata Metadata { get; }
    public OperationType OperationType => OperationType.Query;
    public IOGraphQueryProvider QueryProvider => throw new NotImplementedException();
    public OGraphQueryOptions QueryOptions => throw new NotImplementedException();

    public bool IsEnabled => throw new NotImplementedException();

    protected virtual void Configure(IOGraphQueryOperationDescriptor descriptor) { }

    public OGraphOperationHandler BuildHandlerChain()
    {
        throw new NotImplementedException();
    }

    Task<IOGraphResult> IOGraphOperation.ExecuteAsync(IOGraphOperationContext context, CancellationToken cancellationToken)
    {
        return Middleware.BuildHandlerChain(Resolver).Invoke(context, cancellationToken);
    }
}
