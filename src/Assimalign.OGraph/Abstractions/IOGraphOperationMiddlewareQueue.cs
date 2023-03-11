using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphOperationMiddlewareQueue : IEnumerable<IOGraphOperationMiddleware>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    void Enqueue(IOGraphOperationMiddleware middleware);
}
