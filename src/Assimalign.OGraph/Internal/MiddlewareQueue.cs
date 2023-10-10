using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

/*IOGraphOperationMiddlewareQueue,
    IOGraphPropertyMiddlewareQueue,
    IOGraphEdgeMiddlewareQueue*/
internal class MiddlewareQueue<TMiddleware> : IEnumerable<TMiddleware>
{
    private readonly Queue<TMiddleware> queue;

    public MiddlewareQueue()
    {
        queue = new Queue<TMiddleware>();
    }

    public bool IsReadOnly { get; set; }
    public int Count => queue.Count;

    public void Dequeue(TMiddleware middleware)
    {
        queue.Dequeue();
    }

    public void Enqueue(TMiddleware middleware)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<TMiddleware> GetEnumerator() => queue.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    public static IOGraphEdgeMiddlewareQueue CreateEdgeQueue()
    {
        return new EdgeQueue();
    }



    internal partial class EdgeQueue : MiddlewareQueue<IOGraphEdgeMiddleware>, 
        IOGraphEdgeMiddlewareQueue
    {

    }
}
