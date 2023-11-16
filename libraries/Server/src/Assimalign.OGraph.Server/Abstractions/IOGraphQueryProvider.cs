using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// If building a custom query provider, it only needs to support filtering, paging, sorting.
/// </remarks>
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
    Task<IOGraphQueryResult> ExecuteAsync(IOGraphQueryProviderContext context, OGraphQueryOptions options, CancellationToken cancellationToken = default);
}