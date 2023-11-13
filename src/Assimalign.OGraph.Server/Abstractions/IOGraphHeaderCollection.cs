using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

public interface IOGraphHeaderCollection : IDictionary<HeaderKey, HeaderValue>
{
    HeaderValue ContentType { get; }
    HeaderValue Accept { get; }
}