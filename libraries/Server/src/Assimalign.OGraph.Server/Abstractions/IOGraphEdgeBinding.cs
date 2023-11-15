using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;

public interface IOGraphEdgeBinding : IOGraphGdmBinding
{
    Task ExecuteAsync(IOGraphEdgeBindingContext context, CancellationToken cancellationToken = default);
}
