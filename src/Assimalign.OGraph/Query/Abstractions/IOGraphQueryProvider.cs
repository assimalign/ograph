using Assimalign.OGraph.Syntax;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;


public interface IOGraphQueryResult
{
    /// <summary>
    /// 
    /// </summary>
    OGraphCollection Data { get; }
}

public interface IOGraphQueryProvider
{
    Type ProviderType { get; }
    void ApplyFilter(FilterQueryNode filter);
    void ApplySorting(SortQueryNode sorting);
    void ApplyPaging(PageQueryNode paging);
    void ApplyProjections(ProjectionQueryNode projection);



    Task<IOGraphQueryResult> ExecuteAsync(CancellationToken cancellationToken = default);
}


public interface IOGraphQueryProvider<TProvider>
{

}

public class QueryablePrrovider : IOGraphQueryProvider
{
    public Type ProviderType => typeof(IQueryable);

    public void ApplyFilter(FilterQueryNode filter)
    {
        throw new NotImplementedException();
    }

    public void ApplyPaging(PageQueryNode paging)
    {
        throw new NotImplementedException();
    }

    public void ApplyProjections(ProjectionQueryNode projection)
    {
        throw new NotImplementedException();
    }

    public void ApplySorting(SortQueryNode sorting)
    {
        throw new NotImplementedException();
    }

    public Task<IOGraphQueryResult> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}