namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphQueryResult : IOGraphResult
{
    /// <summary>
    /// 
    /// </summary>
    long Count { get; }
    /// <summary>
    /// The query error the occurred.
    /// </summary>
    IOGraphError Error { get; }
    /// <summary>
    /// 
    /// </summary>
    object Data { get; }
}