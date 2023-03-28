namespace Assimalign.OGraph;

public readonly struct QueryResultPageInfo : IOGraphQueryResultPageInfo
{
    public QueryResultPageInfo(long totalCount, bool hasNext = false, bool hasPrevious = false)
    {
        this.TotalCount = totalCount;
        this.HasNext = hasNext;
        this.HasPrevious = hasPrevious;
    }

    public long TotalCount { get; }
    public bool HasNext { get; }
    public bool HasPrevious { get; }
}
