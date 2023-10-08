using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Results.Operations;

public abstract class OperationResult : IOGraphOperationResult
{



    public abstract Task ExecuteAsync(IOGraphExecutorContext context, CancellationToken cancellationToken = default);
}


public sealed class OperationResultBuilder
{
    public OperationResultBuilder()
    {
        
    }

    public OperationResultBuilder WithHeader(string key, HeaderValue value)
    {
        throw new NotImplementedException();
    }
}