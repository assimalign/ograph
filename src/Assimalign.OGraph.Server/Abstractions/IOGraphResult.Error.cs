namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphErrorResult : IOGraphResult
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphError Error { get; }
}
