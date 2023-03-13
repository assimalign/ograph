
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Syntax;

internal class QueryableQueryProvider<T> : IOGraphQueryProvider
{
    public Type ProviderType => typeof(IQueryable<T>);

    public void ApplyFiltering(FilterQueryNode filter, OGraphQueryContext context)
    {
        throw new NotImplementedException();
    }

    public void ApplyPaging(PageQueryNode paging, OGraphQueryContext context)
    {
        throw new NotImplementedException();
    }

    public void ApplyProjections(ProjectionQueryNode projection, OGraphQueryContext context)
    {
        throw new NotImplementedException();
    }

    public void ApplySorting(SortQueryNode sorting, OGraphQueryContext context)
    {
        throw new NotImplementedException();
    }

    public Task<IOGraphQueryResult> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
