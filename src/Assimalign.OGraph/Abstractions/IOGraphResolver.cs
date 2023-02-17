using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphResolver
{

    Task<IOGraphResolverResult> InvokeAsync(IOGraphResolverContext context, CancellationToken cancellationToken = default);

}
