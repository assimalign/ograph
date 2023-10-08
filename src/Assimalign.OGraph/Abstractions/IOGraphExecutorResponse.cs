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
    /// 
    /// </summary>
    Stream Body { get; }
}
