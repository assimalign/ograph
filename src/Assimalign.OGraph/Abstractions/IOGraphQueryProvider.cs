
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assimalign.OGraph;

public interface IOGraphQueryProvider
{


    Type ElementType { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="options"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IOGraphQueryResult> ExecuteAsync(IOGraphQueryContext context, OGraphQueryOptions options, CancellationToken cancellationToken = default);
}