using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphPropertyMiddlewareQueue : Queue<IOGraphPropertyMiddleware>,
    IOGraphPropertyMiddlewareQueue
{

    public void Add(IOGraphPropertyMiddleware middleware)
    {
        base.Enqueue(middleware);
    }
}
