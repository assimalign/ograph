using System.Collections;
using System.Collections.Generic;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphQueryResult
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphError Error { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphQueryResultPageInfo PageInfo { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphQueryResultNodeCollection Nodes { get; }
}