using System;
using System.IO;

namespace Assimalign.OGraph;

/// <summary>
/// Represents the incoming HTTP Request.
/// </summary>
public interface IOGraphExecutorRequest
{
    /// <summary>
    /// Gets the host.
    /// </summary>
    Host Host { get; }
    /// <summary>
    /// Get the request path.
    /// </summary>
    Path Path { get; }
    /// <summary>
    /// Gets the request method.
    /// </summary>
    Method Method { get; }
    /// <summary>
    /// Gets the request body.
    /// </summary>
    Stream Body { get; }
    /// <summary>
    /// Get the request query param collection.
    /// </summary>
    IOGraphExecutorQueryCollection Query { get; }
    /// <summary>
    /// Gets the request header collection.
    /// </summary>
    IOGraphExecutorHeaderCollection Headers { get; }
}