using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public readonly struct QueryResultEdgeCollection : IOGraphQueryResultEdgeCollection
{

    private readonly Dictionary<string, IOGraphQueryResult> edges;




    public IEnumerator<KeyValuePair<string, IOGraphQueryResult>> GetEnumerator() => this.edges.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
