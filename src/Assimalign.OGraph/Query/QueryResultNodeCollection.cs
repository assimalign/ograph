using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.OGraph;


public readonly struct QueryResultNodeCollection : IOGraphQueryResultNodeCollection
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