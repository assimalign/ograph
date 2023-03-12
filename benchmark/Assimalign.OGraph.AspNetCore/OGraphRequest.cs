using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Assimalign.OGraph.AspNetCore;

using Assimalign.OGraph.Execution;

public sealed class OGraphRequest : IOGraphRequest
{

    public OGraphRequest() { }

    internal OGraphRequest(HttpRequest httpRequest)
    {
        this.Method = httpRequest.Method;
        this.Body = httpRequest.Body;
    }

    public Route Route { get; init; }

    public Method Method { get; init; }

    public IOGraphQueryCollection? Query { get; init; }

    public IOGraphHeaderCollection? Headers { get; init; }

    public Stream? Body { get; init; }
}
