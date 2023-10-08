using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public abstract partial class OGraphResult : IOGraphOperationResult, IOGraphEdgeResult
{
    public abstract Task ExecuteAsync(IOGraphExecutorContext context, CancellationToken cancellationToken = default);


    #region Success Results
    public static OGraphResultBuilder Ok()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Bad Request

    public static OGraphResultBuilder NotFound()
    {
        throw new NotImplementedException();
    }

   
    #endregion

}
