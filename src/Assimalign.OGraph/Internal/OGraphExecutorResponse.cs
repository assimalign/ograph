using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphExecutorResponse : IOGraphExecutorResponse
{
    public OGraphExecutorResponse()
    {
        this.Headers = new OGraphHeaderCollection();
        this.Body = new MemoryStream();
    }
    public StatusCode StatusCode { get; set; }
    public IOGraphHeaderCollection Headers { get; }
    public Stream Body { get; }
}
