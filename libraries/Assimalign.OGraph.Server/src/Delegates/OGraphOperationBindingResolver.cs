using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
/// <param name="context"></param>
/// <param name="cancellationToken"></param>
/// <returns></returns>
public delegate Task<IOGraphResult> OGraphOperationBindingResolver(
    IOGraphOperationBindingContext context, 
    CancellationToken cancellationToken);
