using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmVertex : IOGraphGdmVertex
{
    public GdmVertex()
    {
        
    }
    public Label Label { get; set; }
    public IOGraphGdmTypeReference? Type { get; set; }
    public IOGraphGdmEdgeReference[] Edges { get; set; }
    public IOGraphGdmMetadata Metadata { get; set; }

    public IOGraphGdmPropertyCollection GetProperties()
    {
        throw new NotImplementedException();
    }
}
