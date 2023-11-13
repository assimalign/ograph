using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public sealed class OGraphHeaderCollection : Dictionary<string, HeaderValue>
{
    public OGraphHeaderCollection() : base(StringComparer.CurrentCultureIgnoreCase) { }

    public OGraphHeaderCollection(Dictionary<string, HeaderValue> headers) : base(headers, StringComparer.CurrentCultureIgnoreCase)
    {
        
    }


    public HeaderValue? Host => TryGetValue("host", out var value) ? value : default;
    public HeaderValue? Accept => TryGetValue("accept", out var value) ? value : default;
    public HeaderValue? ContentType => TryGetValue("content-type", out var value) ? value : default;
    public HeaderValue? ContentLength => TryGetValue("content-length", out var value) ? value : default;

}
