using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class QueryProvider : IOGraphQueryProvider
{
    private static readonly HashSet<QueryProviderStrategy> strategies = new();

    static QueryProvider()
    {
        
    }

    internal static void AddStrategy<TStrategy>() where TStrategy : QueryProviderStrategy, new()
    {
        strategies.Add(new TStrategy());
    }

    public Type ElementType { get; init; } = default!;

    public Task ExecuteAsync(IOGraphQueryProviderContext context, OGraphQueryOptions options, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    Task<IOGraphQueryResult> IOGraphQueryProvider.ExecuteAsync(IOGraphQueryProviderContext context, OGraphQueryOptions options, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }



    /* Execution Plan #1
       1.       Get Projections
       1.1          Invoke Projections
       1.2          
     
     
     */
}