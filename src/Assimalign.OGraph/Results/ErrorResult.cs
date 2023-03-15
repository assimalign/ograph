using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace Assimalign.OGraph;

public sealed class ErrorResult : IOGraphOperationResult
{
    IOGraphType IOGraphOperationResult.Type => throw new NotImplementedException();

    Task IOGraphOperationResult.ExecuteAsync(IOGraphOperationContext context, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
