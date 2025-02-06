using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Elements;

using Assimalign.OGraph.Gdm.Internal;


[DebuggerDisplay("Vertex = {Label}")]
public class GdmVertex : IOGraphGdmVertex
{
    private readonly Action<IOGraphGdmVertexDescriptor> configure;

    internal Label label;
    internal IOGraphGdmType? type;
    internal IOGraphGdmGraph? graph;

    public GdmVertex() : this(descriptor => { }) { }
    GdmVertex(Action<IOGraphGdmVertexDescriptor> configure)
    {
        if (configure is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(configure));
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
    public IOGraphGdmType Type => type!;
    public IOGraphGdmEdgeCollection Edges { get; } = new GdmEdgeCollection();
    public IOGraphGdmOperationCollection Operations { get; } = new GdmVertexOperationCollection();
    public IOGraphGdmMetadata Meta { get; } = new GdmMetadata();
    public IOGraphGdmGraph Graph => graph!;
    public GdmElementKind ElementKind => GdmElementKind.Vertex;

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
