using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.AspNetCore.Internal;

internal class ExecutorHeaderCollection : Dictionary<HeaderKey, HeaderValue>, IOGraphExecutorHeaderCollection
{
    public HeaderValue ContentType { get; set; }
    public HeaderValue ContentLength { get; set; }
    public HeaderValue Accept { get; set; }
    public HeaderValue AcceptEncoding { get; set; }
}
