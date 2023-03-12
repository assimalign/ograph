using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Execution;


/// <summary>
/// 
/// </summary>
public interface IOGraphExecutor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IOGraphResponse> ExecuteAsync(IOGraphRequest request, CancellationToken cancellationToken = default);
}



internal struct Content
{



    public ICollection<KeyValuePair<string, object>> Data { get; }

}