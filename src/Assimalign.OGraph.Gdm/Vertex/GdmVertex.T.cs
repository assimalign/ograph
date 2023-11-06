using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm Vertex: {Label}")]
public class GdmVertex<T> : IOGraphGdmVertex
    where T : class,new()
{
    private readonly Action<IOGraphGdmVertexDescriptor<T>>? configure;
    private readonly IList<IOGraphGdmVertexBinding> bindings = new List<IOGraphGdmVertexBinding>();

    internal Label label;
    internal GdmTypeReference? type;

    private GdmVertex(Action<IOGraphGdmVertexDescriptor<T>> configure) : this() => this.configure = configure;
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

    protected virtual void Configure(IOGraphGdmVertexDescriptor<T> descriptor)
    {
        configure?.Invoke(descriptor);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static GdmVertex<T> Create<T>(Action<IOGraphGdmVertexDescriptor<T>> configure) where T : class, new()
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }
        return new GdmVertex<T>(configure);
    }
}