using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;


using Assimalign.OGraph.Syntax;

internal class QueryableQueryProvider : IOGraphQueryProvider
{
    public Type ElementType => typeof(IQueryable);

    Task<IOGraphQueryResult> IOGraphQueryProvider.ExecuteAsync(IOGraphQueryContext context, OGraphQueryOptions options, CancellationToken cancellationToken = default)
    {
        if (context is QueryableQueryContext ctx)
        {
            return ExecuteAsync(ctx, options, cancellationToken);
        }

        throw new ArgumentException();
    }


    public Task<IOGraphQueryResult> ExecuteAsync(QueryableQueryContext context, OGraphQueryOptions options, CancellationToken cancellationToken = default)
    {
        var providerGenericType = context.Queryable.ElementType;
        var provider = (IOGraphQueryProvider)typeof(QueryableQueryProvider<>).MakeGenericType(providerGenericType);

        return provider.ExecuteAsync(context, options, cancellationToken);
    }
}
