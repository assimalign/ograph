using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmOperationBindingMiddlewareQueue : IEnumerable<IOGraphGdmOperationBindingMiddleware>
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
    void Enqueue(IOGraphGdmOperationBindingMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    void Dequeue(IOGraphGdmOperationBindingMiddleware middleware);
}
