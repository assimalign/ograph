using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphPropertyMiddleware
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    ValueTask<IOGraphPropertyResult> InvokeAsync(IOGraphPropertyContext context, OGraphPropertyHandler next);
}
