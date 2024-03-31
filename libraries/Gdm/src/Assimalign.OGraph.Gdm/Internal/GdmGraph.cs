using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmGraph : IOGraphGdmGraph
{
    public IEnumerable<IOGraphGdmVertex> Vertices => throw new NotImplementedException();

    public IEnumerable<IOGraphGdmEdge> Edges => throw new NotImplementedException();

    public IEnumerable<IOGraphGdmBinding> Bindings => throw new NotImplementedException();

    public Label Label => throw new NotImplementedException();

    public GdmElementKind ElementKind => throw new NotImplementedException();

    public void Bind(IOGraphGdmBinding binding)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmEdge GetEdge(Label label)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertex GetVertex(Label label)
    {
        throw new NotImplementedException();
    }
}
