
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Syntax;

internal class ObjectQueryProvider<T> : IOGraphQueryProvider
{
    public Type ProviderType => typeof(T);

    public void ApplyFiltering(FilterQueryNode filter, OGraphQueryContext context)
    {
        throw new InvalidOperationException("Filtering of a Complex Type. Only Projections are permitted.");
    }

    public void ApplyPaging(PageQueryNode paging, OGraphQueryContext context)
    {
        throw new InvalidOperationException("Paging of a Complex Type. Only Projections are permitted.");
    }

    public void ApplyProjections(ProjectionQueryNode projection, OGraphQueryContext context)
    {
        throw new NotImplementedException();
    }

    public void ApplySorting(SortQueryNode sorting, OGraphQueryContext context)
    {
        throw new InvalidOperationException("Sorting of a Complex Type. Only Projections are permitted.");
    }

    public Task<IOGraphQueryResult> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
