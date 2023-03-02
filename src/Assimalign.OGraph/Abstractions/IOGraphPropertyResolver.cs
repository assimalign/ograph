using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphPropertyResolver
{

    ValueTask<IOGraphPropertyResult> InvokeAsync(IOGraphPropertyResolverContext context, CancellationToken cancellationToken = default);
}
