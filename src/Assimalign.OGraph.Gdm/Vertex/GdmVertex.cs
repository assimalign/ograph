using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm Vertex: {Label}")]
public class GdmVertex : IOGraphGdmVertex
{
    private readonly Action<IOGraphGdmVertexDescriptor>? configure;
    private readonly IList<IOGraphGdmVertexBinding> bindings = new List<IOGraphGdmVertexBinding>();

    internal Label label;
    internal GdmTypeReference? type;

    private GdmVertex(Action<IOGraphGdmVertexDescriptor> configure) : this() => this.configure = configure;
    public GdmVertex()
    {
        Configure(new GdmVertexDescriptor(this));
    }

    protected virtual void Configure(IOGraphGdmVertexDescriptor descriptor)
    {
        configure?.Invoke(descriptor);
    }

    public Label Label => label;
    public IOGraphGdmTypeReference Type => type!;
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static GdmVertex Create(Action<IOGraphGdmVertexDescriptor> configure)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        return new GdmVertex(configure);
    }
}
