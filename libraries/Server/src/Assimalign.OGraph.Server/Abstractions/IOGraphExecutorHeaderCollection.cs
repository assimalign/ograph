using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphExecutorHeaderCollection : IDictionary<HeaderKey, HeaderValue>
{
    /// <summary>
    /// 
    /// </summary>
    HeaderValue ContentType { get; set; }
    /// <summary>
    /// 
    /// </summary>
    HeaderValue ContentLength { get; set; }
    /// <summary>
    /// 
    /// </summary>
    HeaderValue Accept { get; set; }
    /// <summary>
    /// 
    /// </summary>
    HeaderValue AcceptEncoding { get; set; }
}