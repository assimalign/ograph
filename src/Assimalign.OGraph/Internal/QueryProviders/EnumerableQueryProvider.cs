using Assimalign.OGraph.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class EnumerableQueryProvider<T> : IOGraphQueryProvider
{
    public Type ElementType => typeof(IEnumerable<T>);

    public Task<IOGraphQueryResult> ExecuteAsync(IOGraphQueryContext context, OGraphQueryOptions options, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
