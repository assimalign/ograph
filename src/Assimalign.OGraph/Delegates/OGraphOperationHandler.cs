using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// A wrapper delegate for executing operation middleware and resolver.
/// </summary>
/// <param name="context"></param>
/// <param name="cancellationToken"></param>
/// <returns></returns>
public delegate Task<IOGraphResult> OGraphOperationHandler(
    IOGraphOperationBindingResolverContext context, 
    CancellationToken cancellationToken = default);