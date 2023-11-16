using System.IO;

namespace Assimalign.OGraph;

/// <summary>
/// Represents the HTTP response.
/// </summary>
public interface IOGraphResponse
{
    /// <summary>
    /// Represents the HTTP status code.
    /// </summary>
    StatusCode StatusCode { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphHeaderCollection Headers { get; }
    /// <summary>
    /// 
    /// </summary>
    Stream Body { get; }
}