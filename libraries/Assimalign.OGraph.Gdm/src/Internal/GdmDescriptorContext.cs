using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

using Elements;

internal class GdmDescriptorContext
{
    public List<Func<GdmGraph, Func<GdmType>>> Types { get; init; }
    public List<Func<GdmGraph, Func<GdmNode>>> Vertices { get; init; }
    public List<Func<GdmGraph, Func<GdmEdge>>> Eges { get; init; }
}
