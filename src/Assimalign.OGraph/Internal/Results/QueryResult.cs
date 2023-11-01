using System;

namespace Assimalign.OGraph.Internal.Results;

internal class QueryResult : IOGraphQueryResult
{
    public long Count { get; set; }
    public StatusCode StatusCode { get; set; }
    public IOGraphError Error => throw new NotImplementedException();
    public Either<QueryObjectData, QueryCollectionData> Data { get; set; }
    IOGraphQueryData IOGraphQueryResult.Data 
    {
        get => Data
            .Match((QueryObjectData data) => data as IOGraphQueryData)
            .Match((QueryCollectionData data) => data as IOGraphQueryData);
    }
}
