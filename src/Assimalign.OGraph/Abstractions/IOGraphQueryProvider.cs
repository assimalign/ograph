using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assimalign.OGraph;

public interface IOGraphQueryProvider
{
    /// <summary>
    /// Represents the runtime type 
    /// </summary>
    Type ElementType { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="options"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ExecuteAsync(IOGraphQueryContext context, OGraphQueryOptions options, CancellationToken cancellationToken = default);
}


public sealed class QueryResult 
{
    /// <summary>
    /// 
    /// </summary>
    public long Total { get; init; }
    /// <summary>
    /// 
    /// </summary>
    public Either<QueryVertexResult, QueryVerticesResult> Nodes { get; init; } 
}

public sealed class QueryError
{

}

public readonly struct QueryVertexResult
{

}

public readonly struct QueryVerticesResult
{

}