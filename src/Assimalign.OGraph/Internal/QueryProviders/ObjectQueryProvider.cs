
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

    public void ApplyFiltering(FilterNode filter, OGraphQueryContext context)
    {
        throw new InvalidOperationException("Filtering of a Complex Type. Only Projections are permitted.");
    }

    public void ApplyPaging(PageNode paging, OGraphQueryContext context)
    {
        throw new InvalidOperationException("Paging of a Complex Type. Only Projections are permitted.");
    }

    public void ApplyProjections(ProjectionNode projection, OGraphQueryContext context)
    {
        throw new NotImplementedException();
    }

    public void ApplySorting(SortNode sorting, OGraphQueryContext context)
    {
        throw new InvalidOperationException("Sorting of a Complex Type. Only Projections are permitted.");
    }

    public Task<IOGraphQueryResult> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
