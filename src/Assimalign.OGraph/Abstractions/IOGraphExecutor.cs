using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphExecutor
{
    /// <summary>
    /// Executes the OGraph request and returns a response to be sent back to the client.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IOGraphExecutorResponse> ExecuteAsync(IOGraphExecutorRequest request, CancellationToken cancellationToken = default);
}