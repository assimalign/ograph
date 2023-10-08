using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public class QueryableResult : OGraphResult
{

    public IOGraphQueryProvider QueryProvider { get; init; }




    public override Task ExecuteAsync(IOGraphExecutorContext context, CancellationToken cancellationToken = default)
    {
       

        QueryProvider.ExecuteAsync()

        throw new NotImplementedException();
    }
}
