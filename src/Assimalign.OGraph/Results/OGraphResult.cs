using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public abstract class OGraphResult : IOGraphOperationResult
{

    public abstract Task ExecuteAsync(IOGraphExecutorContext context, CancellationToken cancellationToken = default);





    public static OGraphResultBuilder Unauthorzed()
    {


        
        return new OGraphResultBuilder(new OGraphUnauthorizedResult());
    
    }


}