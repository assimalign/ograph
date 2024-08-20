using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Vertex = {Label}")]
public class GdmVertex<T> : IOGraphGdmVertex
    where T : class, new()
{
    private readonly Action<IOGraphGdmVertexDescriptor<T>> configure;
    private readonly IList<IOGraphGdmBinding> bindings = new List<IOGraphGdmBinding>();

    internal Label label;
    internal IOGraphGdmTypeReference? type;

    public GdmVertex() : this(descriptor => { }) { }
    GdmVertex(Action<IOGraphGdmVertexDescriptor<T>> configure)
    {
        if (configure is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(configure));
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

    public Label Label => label;
    public IOGraphGdmTypeReference Type => type!;
    public IOGraphGdmEdgeReferenceCollection Edges { get; } = new GdmEdgeReferenceCollection();
    public IOGraphGdmMetadata Metadata { get; } = new GdmMetadata();
    public GdmElementKind ElementKind => GdmElementKind.Vertex;

    #region Explicit Implementations
    IEnumerable<IOGraphGdmBinding> IOGraphGdmBindingElement.Bindings => bindings;
    void IOGraphGdmBindingElement.Bind(IOGraphGdmBinding binding)
    {
        if (binding is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(binding));
        }
        if (this.HasBinding(binding.Label))
        {
            ThrowHelper.ThrowInvalidOperationException($"The element already contains a binding with the label: {binding.Label}");
        }
        bindings.Add(binding);
    }
    void IOGraphGdmBindingElement.Unbind(IOGraphGdmBinding binding)
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