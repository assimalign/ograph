
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
    public Type ElementType => typeof(T);

    public Task<IOGraphQueryResult> ExecuteAsync(IOGraphQueryContext context, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
