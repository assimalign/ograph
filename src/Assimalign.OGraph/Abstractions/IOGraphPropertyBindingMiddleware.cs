using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphPropertyBindingMiddleware
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    ValueTask<IOGraphResult> InvokeAsync(IOGraphPropertyBindingResolverContext context, OGraphPropertyHandler next);
}
