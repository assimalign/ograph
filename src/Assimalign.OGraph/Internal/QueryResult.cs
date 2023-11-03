using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class QueryResult : IOGraphResult
{
    public StatusCode StatusCode => throw new NotImplementedException();

    public Task ExecuteAsync(IOGraphExecutorContext context, CancellationToken cancellationToken = default)
    {


        throw new NotImplementedException();
    }
}
