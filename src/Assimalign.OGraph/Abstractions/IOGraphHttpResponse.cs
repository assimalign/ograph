using System;
using System.IO;

namespace Assimalign.OGraph;

/// <summary>
/// Represents an HTTP Response.
/// </summary>
public interface IOGraphHttpResponse
{
    /// <summary>
    /// 
    /// </summary>
    StatusCode StatusCode { get; set; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphHttpHeaderCollection Headers { get; }
    /// <summary>
    /// 
    /// </summary>
    Stream Body { get; }
}
