using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

public interface IOGraphOperationMiddlewareQueue : IEnumerable<IOGraphOperationMiddleware>
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
    void Enqueue(IOGraphOperationMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    void Dequeue(IOGraphOperationMiddleware middleware);
    /// <summary>
    /// Builds a handler that create invocation chain to execute middleware and resolver.
    /// </summary>
    /// <returns></returns>
    OGraphOperationHandler BuildHandlerChain(IOGraphOperationResolver resolver);
}
