using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
/// <param name="context"></param>
/// <returns></returns>
public delegate Task<IOGraphResult> OGraphGdmPropertyBindingResolver(
    IOGraphGdmPropertyBindingContext context, 
    CancellationToken cancellationToken);