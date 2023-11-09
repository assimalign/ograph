using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphOperationBindingMiddleware
{
   
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    Task<IOGraphResult> InvokeAsync(IOGraphOperationBindingResolverContext context, CancellationToken cancellationToken = default, OGraphOperationHandler next);
}
