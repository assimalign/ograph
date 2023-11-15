using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphQueryCollection : IDictionary<QueryKey, QueryValue>
{
    /// <summary>
    /// 
    /// </summary>
    QueryValue Query { get; }
}