using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;


public class QueryResult : IOGraphQueryResult
{
    private readonly IEnumerable enumerable;
    public QueryResult(IEnumerable enumerable, long total, long count)
    {
        this.enumerable = enumerable;
        this.Total = total;
        this.Count = count;
    }

    public StatusCode StatusCode => 200;
    public long Total { get; }
    public long Count { get; }
    public IEnumerator GetEnumerator() => enumerable.GetEnumerator();
}

public class QueryResult<T> : QueryResult, IOGraphQueryResult<T>
{

    public QueryResult()
    {
        
    }
    public long Total => throw new NotImplementedException();

    public long Count => throw new NotImplementedException();

    public StatusCode StatusCode => throw new NotImplementedException();

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
