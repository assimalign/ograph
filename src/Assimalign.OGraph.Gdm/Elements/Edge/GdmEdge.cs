using Assimalign.OGraph.Gdm.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

internal class GdmEdge<TSource, TTarget> : IOGraphGdmEdge
    where TSource : class, new()
    where TTarget : class, new()
{
    public CardinalityType Cardinality => throw new NotImplementedException();
    public IOGraphGdmVertexReference Source => throw new NotImplementedException();
    public IOGraphGdmVertexReference Target => throw new NotImplementedException();
    public IOGraphGdmMetadata Metadata => throw new NotImplementedException();

    public Label Label => throw new NotImplementedException();

    public GdmElementType ElementType => GdmElementType.Edge;

    IEnumerable<IOGraphGdmBinding> IOGraphGdmBindingElement.Bindings => throw new NotImplementedException();

    void IOGraphGdmBindingElement.Bind(IOGraphGdmBinding binding)
    {
        throw new NotImplementedException();
    }
}
