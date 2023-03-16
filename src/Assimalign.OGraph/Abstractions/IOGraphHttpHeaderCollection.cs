using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

public interface IOGraphHttpHeaderCollection : IDictionary<string, HeaderValue>
{
    HeaderValue? Host { get; }
    HeaderValue? Accept { get; }
    HeaderValue? ContentType { get; }
    HeaderValue? ContentLength { get; }
}
