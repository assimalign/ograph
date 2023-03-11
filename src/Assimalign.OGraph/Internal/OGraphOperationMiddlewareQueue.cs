using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphOperationMiddlewareQueue : IOGraphOperationMiddlewareQueue
{

    private Queue<IOGraphOperationMiddleware> queue;

    public OGraphOperationMiddlewareQueue()
    {
        this.queue = new Queue<IOGraphOperationMiddleware>();
    }


    public bool IsReadOnly { get; set; }

    public int Count => queue.Count;

    public void Dequeue(IOGraphOperationMiddleware middleware)
    {
        AssertIsReadOnly();

        if (middleware is null)
        {
            throw new ArgumentNullException(nameof(middleware));
        }

        var queue = new Queue<IOGraphOperationMiddleware>();

        foreach (var item in this.queue)
        {
            if (!item.Equals(middleware))
            {
                queue.Enqueue(item);
            }
        }

        if ((this.queue.Count - 1) != queue.Count)
        {
            throw new InvalidOperationException("The provided middleware does not exist.");
        }

        this.queue = queue;
    }

    public void Enqueue(IOGraphOperationMiddleware middleware)
    {
        AssertIsReadOnly();

        if (middleware is null)
        {
            throw new ArgumentNullException(nameof(middleware));
        }

        queue.Enqueue(middleware);
    }

    public IEnumerator<IOGraphOperationMiddleware> GetEnumerator() => this.queue.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();


    private void AssertIsReadOnly()
    {
        if (IsReadOnly)
        {
            throw new InvalidOperationException("The Middleware Queue is ReadOnly.");
        }
    }
}
