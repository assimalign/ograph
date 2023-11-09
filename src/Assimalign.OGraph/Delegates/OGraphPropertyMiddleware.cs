using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;


/// <summary>
/// 
/// </summary>
/// <param name="context"></param>
/// <param name="cancellationToken"></param>
/// <param name="next"></param>
/// <returns></returns>
public delegate ValueTask<IOGraphResult> OGraphPropertyMiddleware(
    IOGraphPropertyBindingResolverContext context, 
    CancellationToken cancellationToken,
    OGraphPropertyHandler next);
