using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public readonly struct QueryResultNode : IOGraphQueryResultNode
{
    private readonly Dictionary<string, object> nodes;

    public QueryResultNode()
    {
        this.nodes = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
        this.Edges = new QueryResultEdgeCollection();
    }


    public void Add(string key, object value)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException(nameof(key));
        }

        nodes.Add(key, value);
    }

    public IOGraphQueryResultEdgeCollection Edges { get; }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => nodes.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}