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

    public void AddBinding(IOGraphGdmBinding binding)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IOGraphGdmBinding> GetBindings()
    {
        throw new NotImplementedException();
    }
}
