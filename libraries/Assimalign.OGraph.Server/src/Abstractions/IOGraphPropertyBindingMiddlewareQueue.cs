using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphPropertyBindingMiddlewareQueue : IEnumerable<IOGraphPropertyBindingMiddleware>
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
    void Enqueue(IOGraphPropertyBindingMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    void Dequeue(IOGraphPropertyBindingMiddleware middleware);
}
