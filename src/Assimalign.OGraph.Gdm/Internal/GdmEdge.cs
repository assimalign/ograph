using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmEdge : IOGraphGdmEdge
{
    private readonly IList<IOGraphGdmEdgeBinding> bindings = new List<IOGraphGdmEdgeBinding>();
    public GdmEdge()
    {
        Metadata = new GdmMetadata();
    }

    public Label Label { get; set; } = default!;
    public CardinalityType Cardinality { get; set; } = default!;
    public IOGraphGdmVertexReference Source { get; set; } = default!;
    public IOGraphGdmVertexReference Target { get; set; } = default!;
    public IOGraphGdmMetadata Metadata { get; }
    public void AddBinding(IOGraphGdmEdgeBinding binding)
    {
        if (binding is null)
        {
            throw new ArgumentNullException(nameof(binding));
        }
        bindings.Add(binding);
    }
    public IEnumerable<IOGraphGdmEdgeBinding> GetBindings()
    {
        return bindings;
    }
}
