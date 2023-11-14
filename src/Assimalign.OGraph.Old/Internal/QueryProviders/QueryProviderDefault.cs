using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal abstract class QueryProviderDefault : IOGraphQueryProvider
{
    public abstract Type ElementType { get; init; }

    public abstract Task<IOGraphQueryResult> ExecuteAsync(QueryProviderContextDefault context, OGraphQueryOptions options, CancellationToken cancellationToken = default);

    Task<IOGraphQueryResult> IOGraphQueryProvider.ExecuteAsync(IOGraphQueryContext context, OGraphQueryOptions options, CancellationToken cancellationToken = default)
    {
        if (context is not QueryProviderContextDefault defaultContext)
        {
            throw new ArgumentException("Invalid query context");
        }

        return ExecuteAsync(defaultContext, options, cancellationToken);
    }

    public void WriteObjectOrElementStart()
    {

    }
}
