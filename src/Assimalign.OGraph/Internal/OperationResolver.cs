using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Gdm;

internal class OperationResolver : IOGraphOperationResolver
{
    private readonly OGraphOperationHandler handler;
    public OperationResolver()
    {
        
    }


    public Task<IOGraphResult> InvokeAsync(IOGraphOperationResolverContext context, CancellationToken cancellationToken = default)
    {
        

        throw new NotImplementedException();
    }

    public Task InvokeAsync(IOGraphGdmVertexBindingContext context, CancellationToken cancellationToken = default)
    {





        throw new NotImplementedException();
    }

    public Task InvokeAsync(IOGraphGdmEdgeBindingContext context, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
