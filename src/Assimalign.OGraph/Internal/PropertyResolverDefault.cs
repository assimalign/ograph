using Assimalign.OGraph.Gdm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class PropertyResolverDefault : IOGraphPropertyResolver
{

    public PropertyResolverDefault()
    {
        
    }




    public ValueTask<IOGraphResult> InvokeAsync(IOGraphPropertyResolverContext context, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<object> InvokeAsync(object context, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task InvokeAsync(IOGraphGdmPropertyBindingContext context, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
