using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public readonly struct QueryResult : IOGraphQueryResult
{
    public QueryResult()
    {
        Nodes = new QueryResultNodeCollection();
    }

    public QueryError Error { get; }
    public QueryResultPageInfo PageInfo { get; }
    public QueryResultNodeCollection Nodes { get; }


    IOGraphError IOGraphQueryResult.Error => this.Error;
    IOGraphQueryResultPageInfo IOGraphQueryResult.PageInfo => this.PageInfo;
    IOGraphQueryResultNodeCollection IOGraphQueryResult.Nodes => this.Nodes;
}