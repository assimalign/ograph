using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class Gdm : IOGraphGdm
{
    public Label Label { get; set; }
    public IOGraphGdmTypeCollection Types { get; set; }
    public IOGraphGdmEdgeCollection Edges { get; set; }
    public IOGraphGdmVertexCollection Vertices { get; set; }
}
