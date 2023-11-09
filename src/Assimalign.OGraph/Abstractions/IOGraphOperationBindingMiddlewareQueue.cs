using System.Collections.Generic;

namespace Assimalign.OGraph;

public interface IOGraphOperationBindingMiddlewareQueue : IEnumerable<IOGraphOperationBindingMiddleware>
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
    void Enqueue(IOGraphOperationBindingMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    void Dequeue(IOGraphOperationBindingMiddleware middleware);
}