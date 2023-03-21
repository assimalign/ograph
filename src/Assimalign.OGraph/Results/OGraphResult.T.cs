using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public abstract class OGraphResult<TData> : IOGraphOperationResult, IOGraphEdgeResult
{

    /// <inheritdoc />
    public abstract TData? Data { get; }

    /// <inheritdoc />
    public virtual IOGraphError? Error { get; }

    /// <inheritdoc />
    public abstract Task ExecuteAsync(IOGraphHttpResponse response, CancellationToken cancellationToken = default);

}