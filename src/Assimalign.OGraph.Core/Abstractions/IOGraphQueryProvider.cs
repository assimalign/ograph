using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphQueryProvider
{
    /// <summary>
    /// Represents the runtime type 
    /// </summary>
    Type ElementType { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="options"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IOGraphQueryResult> ExecuteAsync(IOGraphQueryContext context, OGraphQueryOptions options, CancellationToken cancellationToken = default);
}