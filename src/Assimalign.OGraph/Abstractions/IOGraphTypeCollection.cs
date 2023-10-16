using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphTypeCollection : ICollection<IOGraphType>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    bool TryGet(Name name, out IOGraphType type);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    bool TryAdd(IOGraphType type);
}