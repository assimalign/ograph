using System;
using System.Linq;

namespace Assimalign.OGraph.Internal;

internal class OperationMiddlewareQueue : MiddlewareQueueBase<IOGraphOperationMiddleware>,
    IOGraphOperationMiddlewareQueue
{
    public OGraphOperationHandler BuildHandlerChain(IOGraphOperationResolver resolver)
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
