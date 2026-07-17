using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class QueryableQueryProviderStrategy : QueryProviderStrategy
{
    public override Type ElementType => throw new NotImplementedException();

    public override Task ExecuteAsync(IOGraphQueryProviderContext context, OGraphQueryOptions options, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}