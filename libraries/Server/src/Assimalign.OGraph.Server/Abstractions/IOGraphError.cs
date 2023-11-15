namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphError
{
    /// <summary>
    /// The unique error code.
    /// </summary>
    string? Code { get; }
    /// <summary>
    /// The error message.
    /// </summary>
    string? Message { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphErrorDetailsCollection? Details { get; }
}
