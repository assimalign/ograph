using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Results
{
    internal abstract class OperationResult : IOGraphOperationResult
    {

        public abstract Task ExecuteAsync(IOGraphExecutorContext context, CancellationToken cancellationToken = default);
    }
}
