using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmEdge : IOGraphGdmEdge
{
    private readonly IList<IOGraphGdmBinding> bindings = new List<IOGraphGdmBinding>();
    
    public GdmEdge()
    {
        Metadata = new GdmMetadata();
    }

    public Label Label { get; set; } = default!;
    public CardinalityType Cardinality { get; set; } = default!;
    public IOGraphGdmVertexReference Source { get; set; } = default!;
    public IOGraphGdmVertexReference Target { get; set; } = default!;
    public IOGraphGdmMetadata Metadata { get; }
    public GdmElementType ElementType => GdmElementType.Edge;

    public void AddBinding(IOGraphGdmBinding binding)
    {
        if (binding is null)
        {
            throw new ArgumentNullException(nameof(binding));
        }
        bindings.Add(binding);
    }
    public IEnumerable<IOGraphGdmBinding> GetBindings()
    {
        return bindings;
    }
}
