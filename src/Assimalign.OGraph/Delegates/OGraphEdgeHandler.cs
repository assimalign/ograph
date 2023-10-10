using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
/// <param name="context"></param>
/// <returns></returns>
public delegate Task<IOGraphResult> OGraphEdgeHandler(IOGraphEdgeContext context, CancellationToken cancellationToken = default);
