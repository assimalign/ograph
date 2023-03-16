using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphHttpExecutor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IOGraphHttpResponse> ExecuteAsync(IOGraphHttpRequest request, CancellationToken cancellationToken = default);
}
