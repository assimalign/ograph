using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
/// <param name="context"></param>
/// <param name="cancellationToken"></param>
/// <returns></returns>
public delegate Task<IOGraphResult> OGraphOperationResolver(
    IOGraphOperationBindingResolverContext context, 
    CancellationToken cancellationToken = default);
