using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmVertex<T> : IOGraphGdmVertex
    where T : class, new()
{
    private readonly IList<IOGraphGdmBinding> bindings = new List<IOGraphGdmBinding>();
    public GdmVertex()
    {
        Metadata = new GdmMetadata();
    }

    public Label Label { get; set; }
    public GdmTypeReference<GdmEntityType<T>> Type { get; set; } = default!;
    IOGraphGdmTypeReference IOGraphGdmVertex.Type => Type;
    public IOGraphGdmEdgeReferenceCollection Edges { get; } = default!;
    public IOGraphGdmMetadata Metadata { get; }
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
