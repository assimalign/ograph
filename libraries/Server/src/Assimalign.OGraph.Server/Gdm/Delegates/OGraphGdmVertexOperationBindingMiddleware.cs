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
public delegate Task<IOGraphResult> OGraphGdmVertexOperationBindingMiddleware(
    IOGraphGdmOperationBindingContext context,
    CancellationToken cancellationToken,
    OGraphGdmVertexOperationBindingResolver next);