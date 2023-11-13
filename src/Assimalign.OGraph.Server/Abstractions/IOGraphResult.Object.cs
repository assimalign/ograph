namespace Assimalign.OGraph;

public interface IOGraphObjectResult : IOGraphResult
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphError Error { get; }
    /// <summary>
    /// 
    /// </summary>
    object? Data { get; }
}
