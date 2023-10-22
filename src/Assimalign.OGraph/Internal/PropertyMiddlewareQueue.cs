using System;
using System.Linq;

namespace Assimalign.OGraph.Internal;

internal class PropertyMiddlewareQueue : OGraphMiddlewareQueueBase<IOGraphPropertyMiddleware>, 
    IOGraphPropertyMiddlewareQueue
{
    public OGraphPropertyHandler BuildHandlerChain(IOGraphPropertyResolver resolver)
    {
        if (resolver is null)
        {
            throw new ArgumentNullException(nameof(resolver));
        }
        
        var index = 0;
        var root = new OGraphPropertyHandler(resolver.InvokeAsync);

        if (Count == 0)
        {
            return root;
        }

        return Chain(root);

        OGraphPropertyHandler Chain(OGraphPropertyHandler root)
        {
            var middleware = queue.Reverse().Skip(index).First();
            var next = new OGraphPropertyHandler((context, cancellationToken) =>
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