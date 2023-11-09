using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Gdm;

internal class PropertyBinding : IOGraphPropertyBinding
{
    public IOGraphPropertyBindingResolver Resolver { get; set; }
    public IOGraphPropertyBindingMiddlewareQueue Middleware { get; }

    public async Task ExecuteAsync(IOGraphPropertyBindingContext context, CancellationToken cancellationToken = default)
    {
        var property = context.Element;

        property.Setter.Invoke(
            default, 
            default);


        throw new NotImplementedException();
    }

    Task IOGraphGdmBinding.ExecuteAsync(IOGraphGdmBindingContext context, CancellationToken cancellationToken = default)
    {
        if (context is not IOGraphPropertyBindingContext)
        {
            ThrowHelper.ThrowInvalidOperationException("");
        }
        return ExecuteAsync((IOGraphPropertyBindingContext)context, cancellationToken);
    }
}
