namespace Assimalign.OGraph;

/// <summary>
/// An OGraph error result.
/// </summary>
public interface IOGraphErrorResult : IOGraphResult
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphError Error { get; }
}
