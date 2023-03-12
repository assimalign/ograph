using System;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// A wrapper delegate for executing operation middleware and resolver.
/// </summary>
/// <param name="context"></param>
/// <returns></returns>
public delegate Task<IOGraphOperationResult> OGraphOperationHandler(IOGraphOperationResolverContext context);