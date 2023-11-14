using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;

public interface IOGraphEdgeBindingContext : IOGraphGdmBindingContext
{
    new IOGraphGdmEdge Element { get; }
}
