using System;
using System.IO;

namespace Assimalign.OGraph;

/// <summary>
/// Represents an HTTP Response.
/// </summary>
public interface IOGraphExecutorResponse
{
    /// <summary>
    /// 
    /// </summary>
    StatusCode StatusCode { get; set;  }
    /// <summary>
    /// A collection of headers to be returned with the response.
    /// </summary>
    IOGraphHeaderCollection Headers { get; }
    /// <summary>
    /// The response body to be written back to the client.
    /// </summary>
    Stream Body { get; }
}
