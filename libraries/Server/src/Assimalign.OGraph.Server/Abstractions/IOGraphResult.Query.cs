using System.Collections;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphQueryResult : IOGraphResult, IEnumerable
{
    /// <summary>
    /// 
    /// </summary>
    long Total { get; }
    /// <summary>
    /// 
    /// </summary>
    long Count { get; }
}