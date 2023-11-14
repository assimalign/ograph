using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmPropertyBindingMiddlewareQueue : IEnumerable<IOGraphGdmPropertyBindingMiddleware>
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
    void Enqueue(IOGraphGdmPropertyBindingMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    void Dequeue(IOGraphGdmPropertyBindingMiddleware middleware);
}
