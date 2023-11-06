using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

public class GdmVertex : IOGraphGdmVertex
{
    private readonly Action<IOGraphGdmVertexDescriptor> configure;
    private readonly IList<IOGraphGdmVertexBinding> bindings = new List<IOGraphGdmVertexBinding>();

    internal Label label;
    internal GdmTypeReference type;

    public GdmVertex()
    {
        Configure(new GdmVertexDescriptor(this));
    }

    private GdmVertex(Action<IOGraphGdmVertexDescriptor> configure) : this()
    {
        this.configure = configure;
    }

    protected virtual void Configure(IOGraphGdmVertexDescriptor descriptor)
    {
        configure?.Invoke(descriptor);
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


    public static GdmVertex Create(Action<IOGraphGdmVertexDescriptor> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        return new GdmVertex(configure);
    }
}
