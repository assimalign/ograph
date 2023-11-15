using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
/// <param name="context"></param>
/// <param name="cancellationToken"></param>
/// <returns></returns>
public delegate Task<IOGraphResult> OGraphGdmVertexOperationBindingResolver(
    IOGraphGdmOperationBindingContext context, 
    CancellationToken cancellationToken);
