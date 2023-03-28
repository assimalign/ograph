using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Results;

internal class OperationErrorResult : OperationResult
{
    public OperationErrorResult(IOGraphError error)
    {
        if (error is null)
        {
            throw new ArgumentNullException(nameof(error));
        }

        this.Error = error;
    }

    public IOGraphError Error { get; }

    public override Task ExecuteAsync(IOGraphExecutorContext context, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
