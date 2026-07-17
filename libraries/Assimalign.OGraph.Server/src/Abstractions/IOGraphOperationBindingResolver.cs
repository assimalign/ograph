using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphOperationBindingResolver
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IOGraphResult> InvokeAsync(IOGraphOperationBindingContext context, CancellationToken cancellationToken);
}
