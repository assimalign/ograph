using System;

namespace Assimalign.OGraph.Server;

public interface IOGraphPropertyResult<out T> : IOGraphPropertyResult
{
    /// <summary>
    /// 
    /// </summary>
    new T Value { get; }
}