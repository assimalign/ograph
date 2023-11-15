using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;


/// <summary>
/// 
/// </summary>
/// <param name="context"></param>
/// <param name="cancellationToken"></param>
/// <param name="next"></param>
/// <returns></returns>
public delegate ValueTask<IOGraphResult> OGraphGdmPropertyBindingMiddleware(
    IOGraphGdmPropertyBindingContext context, 
    CancellationToken cancellationToken,
    OGraphGdmPropertyBindingResolver next);
