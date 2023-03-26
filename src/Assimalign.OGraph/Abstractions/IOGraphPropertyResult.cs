namespace Assimalign.OGraph;

public interface IOGraphPropertyResult
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphError? Error { get; }
    /// <summary>
    /// 
    /// </summary>
    object? Value { get; }
}
