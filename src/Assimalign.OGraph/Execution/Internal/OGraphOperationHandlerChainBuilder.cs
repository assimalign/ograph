using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphOperationHandlerChainBuilder
{
    private int index;
    private readonly IList<IOGraphOperationMiddleware> collection;

    public OGraphOperationHandlerChainBuilder(IOGraphOperationMiddlewareQueue queue)
    {
        this.collection = queue.Reverse().ToList();
    }

    public OGraphOperationHandler GetChain(OGraphOperationHandler initial)
    {
        var middleware = collection.Skip(index).First();
        var handler = new OGraphOperationHandler(ctx =>
        {
            return middleware.InvokeAsync(ctx, initial);
        });
        if (index < collection.Count - 1)
        {
            index++;
            return GetChain(handler);
        }

        index = 0;

        return handler;
    }
}
