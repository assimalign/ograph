using System.IO;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphResponse
{
    /// <summary>
    /// 
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