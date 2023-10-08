using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Assimalign.OGraph.AspNetCore;

public sealed class OGraphRequest : IOGraphExecutorRequest
{

    public OGraphRequest() { }

    internal OGraphRequest(HttpRequest httpRequest)
    {
        this.Method = httpRequest.Method;
        this.Body = httpRequest.Body;
        this.Path = httpRequest.Path.Value;
        this.Query = new OGraphQueryParamCollection(httpRequest.Query.ToDictionary(k => k.Key, v =>
        {
            return new QueryValue(v.Value);
        }));
        this.Headers = new OGraphHeaderCollection(httpRequest.Headers.ToDictionary(k => k.Key, v =>
        {
            return new HeaderValue(v.Value.ToString());
        }));

    }

    public Host Host { get; init; }
    public Route Route { get; init; }

    public Method Method { get; init; }

    public IOGraphQueryParamCollection? Query { get; init; }

    public IOGraphHeaderCollection? Headers { get; init; }

    public Stream? Body { get; init; }

    public Path Path { get; init; }
}
