using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// The idea of using the result interface is that was pass <see cref="IOGraphExecutorResponse.Body"/> to each
/// 
/// Resolvers may execute asychronously but data write must be doen schronously (after data resolution)
/// </remarks>
public interface IOGraphOperationResult
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ExecuteAsync(IOGraphExecutorContext context, CancellationToken cancellationToken = default);
}