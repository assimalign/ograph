using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public sealed class OGraphUnauthorizedResult : OGraphResult
{
    public override Task ExecuteAsync(IOGraphExecutorContext context, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
