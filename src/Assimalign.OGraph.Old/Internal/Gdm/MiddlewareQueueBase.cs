using System;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.OGraph.Internal;

internal abstract class MiddlewareQueueBase<TMiddleware> : IEnumerable<TMiddleware>
{
    protected Queue<TMiddleware> queue;

    public MiddlewareQueueBase()
    {
        this.queue = new();
    }
    public bool IsReadOnly { get; set; }
    public int Count => queue.Count;
    public void Dequeue(TMiddleware middleware)
    {
        AssertIsReadOnly();

        if (middleware is null)
        {
            throw new ArgumentNullException(nameof(middleware));
        }

        var queue = new Queue<TMiddleware>();

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
    public void Enqueue(TMiddleware middleware)
    {
        AssertIsReadOnly();

        if (middleware is null)
        {
            throw new ArgumentNullException(nameof(middleware));
        }

        queue.Enqueue(middleware);
    }



    public IEnumerator<TMiddleware> GetEnumerator() => this.queue.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();


    private void AssertIsReadOnly()
    {
        if (IsReadOnly)
        {
            throw new InvalidOperationException("The Middleware Queue is ReadOnly.");
        }
    }
}