using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
/// <param name="context"></param>
/// <returns></returns>
public delegate ValueTask<IOGraphResult> OGraphPropertyResolver(
    IOGraphPropertyBindingResolverContext context, 
    CancellationToken cancellationToken = default);