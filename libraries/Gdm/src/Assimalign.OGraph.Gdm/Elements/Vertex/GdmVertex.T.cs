using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm = {Label} Vertex")]
public partial class GdmVertex<T> : IOGraphGdmVertex
    where T : class, new()
{
    private readonly Action<IOGraphGdmVertexDescriptor<T>> configure;
    private readonly IList<IOGraphGdmBinding> bindings = new List<IOGraphGdmBinding>();

    public GdmVertex() : this(descriptor => { }) { }
    GdmVertex(Action<IOGraphGdmVertexDescriptor<T>> configure)
    {
        if (configure is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        this.configure = configure;
        this.Configure(new GdmVertexDescriptor<T>(this));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    protected virtual void Configure(IOGraphGdmVertexDescriptor<T> descriptor)
    {
        configure?.Invoke(descriptor);
    }

    internal Label Label { get; set; }
    internal IOGraphGdmTypeReference Type { get; set; } = default!;
    public IOGraphGdmEdgeReferenceCollection Edges { get; } = new GdmEdgeReferenceCollection();
    public IOGraphGdmMetadata Metadata { get; } = new GdmMetadata();
    public GdmElementType ElementType => GdmElementType.Vertex;

    #region Explicit Implementations
    Label IOGraphGdmElement.Label => Label;
    IOGraphGdmTypeReference IOGraphGdmVertex.Type => Type;
    IEnumerable<IOGraphGdmBinding> IOGraphGdmBindingElement.Bindings => bindings;
    void IOGraphGdmBindingElement.Bind(IOGraphGdmBinding binding)
    {
        if (binding is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(binding));
        }
        bindings.Add(binding);
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static GdmVertex<T> Create(Action<IOGraphGdmVertexDescriptor<T>> configure) 
    {
        return new GdmVertex<T>(configure);
    }

    #region Overload Members

    /// <inheritdoc />
    public override string ToString()
    {
        return Label;
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

    #endregion
}