namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphQueryResult
{
    /// <summary>
    /// An execution error.
    /// </summary>
    IOGraphError? Error { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphQueryResultPageInfo? PageInfo { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphQueryResultNodeCollection? Nodes { get; }
}