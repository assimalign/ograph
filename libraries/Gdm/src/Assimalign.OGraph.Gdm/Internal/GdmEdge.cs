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
    public IOGraphGdmVertexReference Source { get; set; } = default!;
    public IOGraphGdmVertexReference Target { get; set; } = default!;
    public IOGraphGdmMetadata Metadata { get; }
    public GdmElementKind ElementKind => GdmElementKind.Edge;
    IEnumerable<IOGraphGdmBinding> IOGraphGdmBindableElement.Bindings => bindings;
    void IOGraphGdmBindableElement.Bind(IOGraphGdmBinding binding)
    {
        if (binding is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(binding));
        }
        bindings.Add(binding);
    }
    void IOGraphGdmBindableElement.Unbind(IOGraphGdmBinding binding)
    {
        if (binding is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(binding));
        }
        if (!bindings.Remove(binding))
        {
            // TODO: Throw error
        }
    }
}
