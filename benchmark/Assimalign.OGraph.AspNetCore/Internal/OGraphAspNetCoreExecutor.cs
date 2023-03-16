using Assimalign.OGraph.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.AspNetCore.Internal;

internal class OGraphAspNetCoreExecutor : OGraphExecutor
{

    public override Task<IOGraphHttpResponse> ExecuteAsync(Name name, IOGraphHttpRequest request, CancellationToken cancellationToken = default)
    {
        return base.ExecuteAsync(name, request, cancellationToken);
    }

}
