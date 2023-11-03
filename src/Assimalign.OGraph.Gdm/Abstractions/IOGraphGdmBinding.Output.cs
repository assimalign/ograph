using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmOutputBinding : IOGraphGdmBinding
{
    Task<object> InvokeAsync(object context, CancellationToken cancellationToken = default);
}
