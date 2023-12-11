using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal abstract class ResultProvider
{


    internal abstract Task HandleAsync(IOGraphResult result, OperationBindingContext context, CancellationToken cancellationToken);


}


internal class QueryResultProvider : ResultProvider
{
    internal override Task HandleAsync(IOGraphResult result, OperationBindingContext context, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}