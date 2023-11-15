using System;
using System.IO;

namespace Assimalign.OGraph.Internal;

internal class ExecutorResponse : IOGraphExecutorResponse
{
    public ExecutorResponse()
    {
        this.Headers = new OGraphHeaderCollection();
        this.Body = new MemoryStream();
    }
    public StatusCode StatusCode { get; set; }
    public IOGraphHeaderCollection Headers { get; }
    public Stream Body { get; }
}
