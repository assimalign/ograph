using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmVertexOperationBinding : IOGraphGdmOperationBinding
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
    public IOGraphGdmOperationBindingResolver Resolver { get; set; } = default!;
    public IOGraphGdmOperationBindingMiddlewareQueue Middleware { get; }
    public async Task ExecuteAsync(IOGraphGdmOperationBindingContext context, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    Task IOGraphGdmBinding.ExecuteAsync(IOGraphGdmBindingContext context, CancellationToken cancellationToken)
    {
        if (context is IOGraphGdmOperationBindingContext bc)
        {
            return ExecuteAsync(bc, cancellationToken);
        }
        throw new InvalidOperationException();
    }
}
