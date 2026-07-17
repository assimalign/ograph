using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmEdge : IOGraphGdmEdge
{
    //private readonly IList<IOGraphGdmBinding> bindings = new List<IOGraphGdmBinding>();
    
    public GdmEdge()
    {
       
    }

    public Label Label { get; set; } = default!;
    public IOGraphGdmVertexReference Source { get; set; } = default!;
    public IOGraphGdmVertexReference Target { get; set; } = default!;
    public IOGraphGdmMetadata Meta { get; } = new GdmMetadata();
    public GdmElementKind ElementKind => GdmElementKind.Edge;
    public IOGraphGdmGraph Graph { get; set; } = default!;
    public IOGraphGdmOperationCollection Operations { get; } = new GdmOperationCollection();
    //IEnumerable<IOGraphGdmBinding> IOGraphGdmBindableElement.Bindings => bindings;
    //void IOGraphGdmBindableElement.Bind(IOGraphGdmBinding binding)
    //{
    //    if (binding is null)
    //    {
    //        ThrowHelper.ThrowArgumentNullException(nameof(binding));
    //    }
    //    bindings.Add(binding);
    //}
    //void IOGraphGdmBindableElement.Unbind(IOGraphGdmBinding binding)
    //{
    //    if (binding is null)
    //    {
    //        ThrowHelper.ThrowArgumentNullException(nameof(binding));
    //    }
    //    if (!bindings.Remove(binding))
    //    {
    //        // TODO: Throw error
    //    }
    //}
}
