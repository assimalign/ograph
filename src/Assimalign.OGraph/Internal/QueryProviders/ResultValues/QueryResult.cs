using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal readonly struct QueryResult : IOGraphQueryResult
{

    public QueryResult()
    {
        Nodes = new QueryResultNodeCollection();
    }

    public QueryResultPageInfo PageInfo { get; }

    [JsonConverter(typeof(NodeCollectionConverter))]
    public QueryResultNodeCollection Nodes { get; }

    IOGraphError IOGraphQueryResult.Error => null;
    IOGraphQueryResultPageInfo IOGraphQueryResult.PageInfo => this.PageInfo;

    [JsonConverter(typeof(NodeCollectionConverter))]
    IOGraphQueryResultNodeCollection IOGraphQueryResult.Nodes => this.Nodes;
}
internal readonly struct QueryResultPageInfo : IOGraphQueryResultPageInfo
{
    public QueryResultPageInfo()
    {
        
    }

    public long TotalCount { get; }
    public bool HasNext { get; }
    public bool HasPrevious { get; }
}
[JsonConverter(typeof(NodeCollectionConverter))]
internal readonly struct QueryResultNodeCollection : IOGraphQueryResultNodeCollection
{
    private readonly IList<IOGraphQueryResultNode> nodes;

    public QueryResultNodeCollection()
    {
        this.nodes = new List<IOGraphQueryResultNode>();
    }

    public void Add(IOGraphQueryResultNode node)
    {
        this.nodes.Add(node);
    }



    public IEnumerator<IOGraphQueryResultNode> GetEnumerator() => this.nodes.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}

[JsonConverter(typeof(NodeConverter))]
internal readonly struct QueryResultNode : IOGraphQueryResultNode
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

internal readonly struct QueryResultEdgeCollection : IOGraphQueryResultEdgeCollection
{

    private readonly Dictionary<string, IOGraphQueryResult> edges;




    public IEnumerator<KeyValuePair<string, IOGraphQueryResult>> GetEnumerator() => this.edges.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}


internal class NodeConverter : JsonConverter<QueryResultNode>
{
    public override QueryResultNode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, QueryResultNode value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        foreach (var item in value)
        {
            writer.WritePropertyName(item.Key);
            writer.WriteStringValue(item.Value.ToString());
        }

        writer.WriteEndObject();
    }
}

internal class NodeCollectionConverter : JsonConverter<QueryResultNodeCollection>
{
    public override QueryResultNodeCollection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, QueryResultNodeCollection value, JsonSerializerOptions options)
    {
        var nodeConverter = options.GetConverter(typeof(QueryResultNode)) as JsonConverter<QueryResultNode>;

        writer.WriteStartArray();

        foreach (var item in value)
        {
            nodeConverter.Write(writer, (QueryResultNode)item, options);
        }


        writer.WriteEndArray();
        
    }
}