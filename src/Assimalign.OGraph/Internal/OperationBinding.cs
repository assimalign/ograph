
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Gdm;

internal class OperationBinding : IOGraphOperationBinding
{
    public OperationBinding()
    {
        
    }

    public Label Label { get; set; }
    public Route Route { get; set; }
    public Method Method { get; set; }
    public IOGraphOperationBindingResolver Resolver { get; set; } = default!;
    public IOGraphOperationBindingMiddlewareQueue Middleware { get; }

    public OGraphOperationHandler GetHandlerChain()
    {
        throw new NotImplementedException();
    }

    public async Task ExecuteAsync(IOGraphOperationBindingContext context, CancellationToken cancellationToken = default)
    {
        var handler = GetHandlerChain();

        var result = await handler.Invoke(default, cancellationToken);



    }

    Task IOGraphGdmBinding.ExecuteAsync(IOGraphGdmBindingContext context, CancellationToken cancellationToken = default)
    {
        if (context is not IOGraphOperationBindingContext)
        {
            ThrowHelper.ThrowInvalidOperationException("");
        }
        return ExecuteAsync((IOGraphOperationBindingContext)context, cancellationToken);
    }




    public OGraphOperationHandler BuildHandlerChain(IOGraphOperationBindingResolver resolver)
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }

        var index = 0;
        var root = new OGraphOperationHandler(resolver.InvokeAsync);

        if (Count == 0)
        {
            return root;
        }
        return Chain(root);

        OGraphOperationHandler Chain(OGraphOperationHandler root)
        {
            var middleware = queue.Reverse().Skip(index).First();
            var next = new OGraphOperationHandler((context, cancellationToken) =>
            {
                return middleware.InvokeAsync(context, root);
            });
            if (index < this.Count - 1)
            {
                index++;
                return Chain(next);
            }
            return next;
        }
    }
}
