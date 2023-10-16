using System.Collections.Generic;

namespace Assimalign.OGraph;

public interface IOGraphEdgeMiddlewareQueue : IEnumerable<IOGraphEdgeMiddleware>
{
    /// <summary>
    /// 
    /// </summary>
    int Count { get; }
    /// <summary>
    /// 
    /// </summary>
    bool IsReadOnly { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    void Enqueue(IOGraphEdgeMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    void Dequeue(IOGraphEdgeMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    OGraphEdgeHandler BuildHandlerChain(IOGraphEdgeResolver resolver);
}
