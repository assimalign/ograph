namespace Assimalign.OGraph;

/// <summary>
/// Extension information regarding the error that occurred.
/// </summary>
public interface IOGraphErrorDetails
{
    /// <summary>
    /// 
    /// </summary>
    public string? Title { get; }
    /// <summary>
    /// 
    /// </summary>
    public string? Message { get; }
}
