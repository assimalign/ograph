using System;
using System.Linq;

namespace Assimalign.OGraph.Internal;

internal class OGraphEdgeMiddlewareQueue : OGraphMiddlewareQueueBase<IOGraphEdgeMiddleware>,
    IOGraphEdgeMiddlewareQueue
{
    public OGraphEdgeHandler BuildHandlerChain(IOGraphEdgeResolver resolver)
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }

        var index = 0;
        var root = new OGraphEdgeHandler(resolver.InvokeAsync);

        if (Count == 0)
        {
            return root;
        }
        return Chain(root);

        OGraphEdgeHandler Chain(OGraphEdgeHandler root)
        {
            var middleware = queue.Reverse().Skip(index).First();
            var next = new OGraphEdgeHandler((context, cancellationToken) =>
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
