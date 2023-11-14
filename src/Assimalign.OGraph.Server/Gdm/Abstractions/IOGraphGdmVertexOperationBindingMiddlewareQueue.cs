using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmVertexOperationBindingMiddlewareQueue : IEnumerable<IOGraphGdmVertexOperationBindingMiddleware>
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
    void Enqueue(IOGraphGdmVertexOperationBindingMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    void Dequeue(IOGraphGdmVertexOperationBindingMiddleware middleware);
}
