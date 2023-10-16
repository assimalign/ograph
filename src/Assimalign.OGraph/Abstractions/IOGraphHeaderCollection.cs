using System.Collections.Generic;

namespace Assimalign.OGraph;

/// <summary>
/// A collection of HTTP headers.
/// </summary>
public interface IOGraphHeaderCollection : IDictionary<string, HeaderValue>
{
    HeaderValue? Host { get; }
    HeaderValue? Accept { get; }
    HeaderValue? ContentType { get; }
    HeaderValue? ContentLength { get; }
}
