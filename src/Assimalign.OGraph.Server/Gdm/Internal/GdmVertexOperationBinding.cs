using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmVertexOperationBinding : IOGraphGdmVertexOperationBinding
{

    public GdmVertexOperationBinding()
    {
        Middleware = new GdmVertexOperationBindingMiddlewareQueue();
    }
    public Label Label { get; set; }
    public Route Route { get; set; }
    public Method Method { get; set; }
    public OperationType OperationType { get; set; }
    public IOGraphGdmTypeReference RequestType { get; set; } = default!;
    public IOGraphGdmTypeReference ResponseType { get; set; } = default!;
    public IOGraphGdmVertexOperationBindingResolver Resolver { get; set; } = default!;
    public IOGraphGdmVertexOperationBindingMiddlewareQueue Middleware { get; }
    public async Task ExecuteAsync(IOGraphGdmVertexOperationBindingContext context, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    Task IOGraphGdmBinding.ExecuteAsync(IOGraphGdmBindingContext context, CancellationToken cancellationToken)
    {
        if (context is IOGraphGdmVertexOperationBindingContext bc)
        {
            return ExecuteAsync(bc, cancellationToken);
        }
        throw new InvalidOperationException();
    }
}
