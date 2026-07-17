using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal abstract class QueryProviderStrategy
{
    public abstract Type ElementType { get; }

    public abstract Task ExecuteAsync(
        IOGraphQueryProviderContext context,
        OGraphQueryOptions options,
        CancellationToken cancellationToken = default);
}
