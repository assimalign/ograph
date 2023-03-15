using System;
using System.IO;

namespace Assimalign.OGraph;

/// <summary>
/// Represents an HTTP Response.
/// </summary>
public interface IOGraphResponse
{
    /// <summary>
    /// 
    /// </summary>
    StatusCode StatusCode { get; set; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphHeaderCollection Headers { get; }
    /// <summary>
    /// 
    /// </summary>
    Stream Body { get; }
}
