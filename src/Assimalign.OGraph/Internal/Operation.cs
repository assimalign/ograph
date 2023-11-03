using Assimalign.OGraph.Gdm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class Operation : IOGraphOperation
{
    public Operation()
    {
        
    }
    public Label Label { get; set; }
    public Route Route { get; set; }
    public Method Method { get; set; }
    public IOGraphOperationResolver Resolver { get; set; } = default!;
    public IOGraphOperationMiddlewareQueue Middleware { get; }

    public OGraphOperationHandler GetHandlerChain()
    {
        throw new NotImplementedException();
    }

    public async Task InvokeAsync(IOGraphGdmVertexBindingContext context, CancellationToken cancellationToken = default)
    {
        if (context is not IOGraphOperationResolverContext opContext)
        {
            throw new Exception();
        }



        var result = await GetHandlerChain()
            .Invoke(opContext, cancellationToken);

        var vertex = opContext.Vertex;
        var vertexType = vertex.Type.Definition;

        


    }
}
