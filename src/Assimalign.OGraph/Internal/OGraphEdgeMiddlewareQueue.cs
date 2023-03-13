using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphEdgeMiddlewareQueue : IOGraphEdgeMiddlewareQueue
{
    public int Count => throw new NotImplementedException();

    public bool IsReadOnly => throw new NotImplementedException();

    public void Dequeue(IOGraphEdgeMiddleware middleware)
    {
        throw new NotImplementedException();
    }

    public void Enqueue(IOGraphEdgeMiddleware middleware)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<IOGraphEdgeMiddleware> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
