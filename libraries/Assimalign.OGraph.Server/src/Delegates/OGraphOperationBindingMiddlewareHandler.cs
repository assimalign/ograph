using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public delegate Task<IOGraphResult> OGraphOperationBindingMiddlewareHandler(
    IOGraphOperationBindingContext context, 
    CancellationToken cancellationToken);
