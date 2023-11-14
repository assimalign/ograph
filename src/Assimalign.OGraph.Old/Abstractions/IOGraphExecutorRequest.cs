using System.IO;

namespace Assimalign.OGraph;

/// <summary>
/// The incomin HTTP request.
/// </summary>
public interface IOGraphExecutorRequest
{
    /// <summary>
    /// The hose of the request.
    /// </summary>
    Host Host { get; }
    /// <summary>
    /// The request path
    /// </summary>
    Path Path { get; }
    /// <summary>
    /// The HTTP method of the request.
    /// </summary>
    Method Method { get; }
    /// <summary>
    /// The request query parameters.
    /// </summary>
    IOGraphQueryParamCollection Query { get; }
    /// <summary>
    /// The request headers.
    /// </summary>
    IOGraphHeaderCollection Headers { get; }
    /// <summary>
    /// The raw request body.
    /// </summary>
    Stream Body { get; }
}
