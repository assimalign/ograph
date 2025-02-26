using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Elements;

public class GdmEdge : IOGraphGdmEdge
{
    public GdmEdge(GdmLabel label, GdmVertex target, GdmVertex source, GdmGraph Graph)
    {
        
    }
    IOGraphGdmVertex IOGraphGdmEdge.Target => throw new NotImplementedException();

    IOGraphGdmVertex IOGraphGdmEdge.Source => throw new NotImplementedException();

    IOGraphGdmOperationCollection IOGraphGdmEdge.Operations => throw new NotImplementedException();

    IOGraphGdmGraph IOGraphGdmEdge.Graph => throw new NotImplementedException();

    GdmLabel IOGraphGdmLabeledElement.Label => throw new NotImplementedException();

    GdmElementKind IOGraphGdmElement.ElementKind => throw new NotImplementedException();

    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => throw new NotImplementedException();
}
