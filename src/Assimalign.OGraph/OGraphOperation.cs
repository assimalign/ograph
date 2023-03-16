using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public abstract class OGraphOperation : IOGraphOperation
{
    protected OGraphOperation()
    {
    }

    public Name Name => throw new NotImplementedException();
    public Route Route => throw new NotImplementedException();
    public Method Method => throw new NotImplementedException();
    public bool IsEnabled => throw new NotImplementedException();
    public IOGraphNode Node => throw new NotImplementedException();
    public IOGraphOperationResolver Resolver => throw new NotImplementedException();
    public IOGraphOperationMiddlewareQueue Middleware => throw new NotImplementedException();
    public IOGraphMetadata Metadata => throw new NotImplementedException();

    public IOGraphQueryProvider QueryProvider => throw new NotImplementedException();

    public OGraphOperationHandler GetResolverChain()
    {
        throw new NotImplementedException();
    }
}


public abstract class OGraphOperation<TNode> : OGraphOperation 
    where TNode : IOGraphNode, new()
{
    protected abstract void Configure(IOGraphOperationDescriptor descriptor);
}
