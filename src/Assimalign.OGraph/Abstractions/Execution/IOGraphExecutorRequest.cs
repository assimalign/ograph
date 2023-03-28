using System;
using System.IO;

namespace Assimalign.OGraph;

public interface IOGraphExecutorRequest
{
    /// <summary>
    /// 
    /// </summary>
    Host Host { get; }
    /// <summary>
    /// The request path
    /// </summary>
    Path Path { get; }    
    /// <summary>
    /// 
    /// </summary>
    Method Method { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphQueryCollection Query { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphHeaderCollection Headers { get; }
    /// <summary>
    /// 
    /// </summary>
    Stream Body { get; }
}
