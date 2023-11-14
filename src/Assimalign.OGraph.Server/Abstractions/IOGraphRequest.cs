using System;
using System.IO;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphRequest
{
    /// <summary>
    /// 
    /// </summary>
    Host Host { get; }
    /// <summary>
    /// 
    /// </summary>
    Path Path { get; }
    /// <summary>
    /// 
    /// </summary>
    Method Method { get; }
    /// <summary>
    /// 
    /// </summary>
    Stream Body { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphQueryCollection Query { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphHeaderCollection Headers { get; }
}