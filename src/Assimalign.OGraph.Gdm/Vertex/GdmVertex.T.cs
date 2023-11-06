using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

public class GdmVertex<T> : IOGraphGdmVertex
    where T : class, IOGraphGdmType, new()
{
    private readonly IList<IOGraphGdmVertexBinding> bindings = new List<IOGraphGdmVertexBinding>();

    internal Label label;
    internal GdmTypeReference<T> type;

    public GdmVertex()
    {
        Configure(new GdmVertexDescriptor<T>(this));
    }

    public Label Label => label;
    public IOGraphGdmTypeReference Type => type;
    public IOGraphGdmEdgeReferenceCollection Edges { get; } = new GdmEdgeReferenceCollection();
    public IOGraphGdmMetadata Metadata { get; } = new GdmMetadata();
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

    protected virtual void Configure(IOGraphGdmVertexDescriptor<T> descriptor) { }
}