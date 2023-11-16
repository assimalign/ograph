using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class MiddlewareQueueBase<TMiddleware> : IEnumerable<TMiddleware>
{

    public int Count { get; set; }
    public bool IsReadOnly { get; set; }


    public void Enqueue(TMiddleware middleware)
    {

    }
    public void Dequeue(TMiddleware middleware)
    {

    }


    public IEnumerator<TMiddleware> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
