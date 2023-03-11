using Assimalign.OGraph;
using Assimalign.OGraph.Execution;

namespace Assimalign.OGraph.AspNetCore;

internal class OGraphHttpRequest : IOGraphRequest
{
    private readonly HttpRequest request;

    public OGraphHttpRequest(HttpRequest request)
    {
        this.request = request;
    }


    public IServiceProvider ServiceProvider { get; init; }
    public IOGraphHeaderCollection Headers => throw new NotImplementedException();

    public Stream Body => request.Body;

    public IOGraphQueryCollection Query => new QueryCollection(request.Query.ToDictionary(x=>x.Key, x=>x.Value.ToString()));


    private class QueryCollection : Dictionary<string, string>, IOGraphQueryCollection
    {
        public QueryCollection(Dictionary<string, string> values) : base(values)
        {
            
        }
    }
}
