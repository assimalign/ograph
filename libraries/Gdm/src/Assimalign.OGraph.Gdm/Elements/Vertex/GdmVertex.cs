using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;


[DebuggerDisplay("Vertex = {Label}")]
public class GdmVertex : IOGraphGdmVertex
{
    private readonly Action<IOGraphGdmVertexDescriptor> configure;
    private readonly IList<IOGraphGdmBinding> bindings = new List<IOGraphGdmBinding>();

    internal Label label;
    internal IOGraphGdmTypeReference? type;

    public GdmVertex() : this(descriptor => { }) { }
    GdmVertex(Action<IOGraphGdmVertexDescriptor> configure)
    {
        if (configure is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        this.configure = configure;
        this.Configure(new GdmVertexDescriptor(this));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    protected virtual void Configure(IOGraphGdmVertexDescriptor descriptor)
    {
        configure.Invoke(descriptor);
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
            GdmThrowHelper.ThrowArgumentNullException(nameof(binding));
        }
        bindings.Add(binding);
    }
    #endregion

    #region Static Members
    /// <summary>
    /// Creates a Vertex from a configurable method.
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static GdmVertex Create(Action<IOGraphGdmVertexDescriptor> configure)
    {
        return new GdmVertex(configure);
    }
    #endregion

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
