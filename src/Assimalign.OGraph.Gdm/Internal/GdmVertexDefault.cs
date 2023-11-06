using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmVertexDefault<T> : IOGraphGdmVertex
    where T : class, new()
{
    private readonly IList<IOGraphGdmVertexBinding> bindings = new List<IOGraphGdmVertexBinding>();
    public GdmVertexDefault()
    {
        Metadata = new GdmMetadata();
    }

    public Label Label { get; set; }
    public GdmTypeReference<GdmEntityType<T>> Type { get; set; } = default!;
    IOGraphGdmTypeReference IOGraphGdmVertex.Type => Type;
    public IOGraphGdmEdgeReferenceCollection Edges { get; } = default!;
    public IOGraphGdmMetadata Metadata { get; }
    public void AddBinding(IOGraphGdmVertexBinding binding)
    {
        if (binding is null)
        {
            throw new ArgumentNullException(nameof(binding));
        }
        bindings.Add(binding);
    }
    public IEnumerable<IOGraphGdmVertexBinding> GetBindings()
    {
        return bindings;
    }
}
