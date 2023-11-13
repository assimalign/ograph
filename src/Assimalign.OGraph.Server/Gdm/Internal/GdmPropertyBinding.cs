using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmPropertyBinding : IOGraphGdmPropertyBinding
{
    public GdmPropertyBinding()
    {
        
    }
    public IOGraphGdmPropertyBindingResolver Resolver { get; }
    public IOGraphGdmPropertyBindingMiddlewareQueue Middleware { get; }

    public Task ExecuteAsync(IOGraphGdmPropertyBindingContext context, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task IOGraphGdmBinding.ExecuteAsync(IOGraphGdmBindingContext context, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
