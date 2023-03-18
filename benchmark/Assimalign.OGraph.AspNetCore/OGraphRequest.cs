using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Assimalign.OGraph.AspNetCore;

public sealed class OGraphRequest : IOGraphHttpRequest
{

    public OGraphRequest() { }

    internal OGraphRequest(HttpRequest httpRequest)
    {
        this.Method = httpRequest.Method;
        this.Body = httpRequest.Body;
    }

    public Route Route { get; init; }

    public Method Method { get; init; }

    public IOGraphHttpQueryCollection? Query { get; init; }

    public IOGraphHttpHeaderCollection? Headers { get; init; }

    public Stream? Body { get; init; }

    public Path Path => throw new NotImplementedException();
}
