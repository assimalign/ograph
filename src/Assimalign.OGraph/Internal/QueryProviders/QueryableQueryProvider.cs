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
    public Type ElementType => typeof(IQueryable);

    public Task<IOGraphQueryResult> ExecuteAsync(IOGraphQueryContext context, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
