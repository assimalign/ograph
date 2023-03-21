using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public abstract class OGraphOperationResult : IOGraphOperationResult
{
    public abstract IOGraphError? Error { get; }

    public abstract Task ExecuteAsync(IOGraphHttpResponse response, CancellationToken cancellationToken = default);
}


public abstract class OGraphOperationResult<T> : OGraphOperationResult
{
    public abstract T Data { get; }
}

public class OGraphOkResult : OGraphOperationResult
{
    public override IOGraphError? Error => throw new NotImplementedException();

    public override Task ExecuteAsync(IOGraphHttpResponse response, CancellationToken cancellationToken = default)
    {
        response.StatusCode = 200;
        throw new NotImplementedException();
    }
}