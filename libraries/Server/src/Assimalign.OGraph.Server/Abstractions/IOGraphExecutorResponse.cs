using System.IO;

namespace Assimalign.OGraph;

/// <summary>
/// Represents the HTTP response.
/// </summary>
public interface IOGraphExecutorResponse
{
    /// <summary>
    /// Represents the HTTP status code.
    /// </summary>
    StatusCode StatusCode { get; set; }
    /// <summary>
    /// The collection of headers to be sent back to the client.
    /// </summary>
    IOGraphExecutorHeaderCollection Headers { get; }
    /// <summary>
    /// The response body to be send back to the client.
    /// </summary>
    Stream Body { get; }
}