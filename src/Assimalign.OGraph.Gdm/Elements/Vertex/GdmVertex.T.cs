using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

using Internal;

[DebuggerDisplay("Gdm Vertex: {Label}")]
public class GdmVertex<T> : IOGraphGdmVertex
    where T : class, new()
{
    private readonly Action<IOGraphGdmVertexDescriptor<T>> configure;
    private readonly IList<IOGraphGdmBinding> bindings = new List<IOGraphGdmBinding>();

    internal Label label;
    internal GdmTypeReference? type;

    public GdmVertex() : this(descriptor => { })
    {
    }
    GdmVertex(Action<IOGraphGdmVertexDescriptor<T>> configure)
    {
        if (configure is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        this.configure = configure;
        this.Configure(new GdmVertexDescriptor<T>(this));
    }

    protected virtual void Configure(IOGraphGdmVertexDescriptor<T> descriptor)
    {
        configure?.Invoke(descriptor);
    }

    public Label Label => label;
    public IOGraphGdmTypeReference Type => type!;
    public IOGraphGdmEdgeReferenceCollection Edges { get; } = new GdmEdgeReferenceCollection();
    public IOGraphGdmMetadata Metadata { get; } = new GdmMetadata();
    public GdmElementType ElementType => GdmElementType.Vertex;
    

    void IOGraphGdmVertex.AddBinding(IOGraphGdmBinding binding)
    {
        if (binding is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(binding));
        }
        bindings.Add(binding);
    }
    IEnumerable<IOGraphGdmBinding> IOGraphGdmVertex.GetBindings()
    {
        return bindings;
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
        return new GdmVertex<T>(configure);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Label, typeof(IOGraphGdmVertex));
    }

    /// <inheritdoc />
    public override bool Equals(object? instance)
    {
        if (instance is not null)
        {
            return GetHashCode() == instance.GetHashCode();
        }
        return false;
    }
}