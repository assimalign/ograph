using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// This is a wrapper interface for whatever host the user implements.
/// </remarks>
public interface IOGraphExecutor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ExecuteAsync(IOGraphExecutorContext context, CancellationToken cancellationToken = default);
}