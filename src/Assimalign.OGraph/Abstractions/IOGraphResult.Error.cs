namespace Assimalign.OGraph;

public interface IOGraphErrorResult : IOGraphResult
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphError Error { get; }
}
