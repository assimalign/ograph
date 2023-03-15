using Assimalign.OGraph.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class QueryableQueryProvider : IOGraphQueryProvider
{
    public Type ProviderType => typeof(IQueryable);

    public void ApplyFiltering(FilterNode filter, OGraphQueryContext context)
    {
        throw new NotImplementedException();
    }

    public void ApplyPaging(PageNode paging, OGraphQueryContext context)
    {
        throw new NotImplementedException();
    }

    public void ApplyProjections(ProjectionNode projection, OGraphQueryContext context)
    {
        throw new NotImplementedException();
    }

    public void ApplySorting(SortNode sorting, OGraphQueryContext context)
    {
        throw new NotImplementedException();
    }

    public Task<IOGraphQueryResult> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
