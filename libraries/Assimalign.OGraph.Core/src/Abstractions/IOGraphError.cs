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
    /// Gets a collection in details regarding the error.
    /// </summary>
    IOGraphErrorDetailsCollection? Details { get; }
}