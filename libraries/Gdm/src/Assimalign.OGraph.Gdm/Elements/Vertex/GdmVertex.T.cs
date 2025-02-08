using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Elements;

using Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Vertex = {Label}")]
public class GdmVertex<T> : IOGraphGdmVertex
    where T : class, new()
{
    private readonly Action<IOGraphGdmVertexDescriptor<T>>? configure;

    #region Constructors

    public GdmVertex()
    {

    }
    internal GdmVertex(Action<IOGraphGdmVertexDescriptor<T>> configure)
    {
        this.configure = configure;

        var descriptor = new GdmVertexDescriptor<T>(this);

        this.Configure(descriptor);

        
    }
    internal GdmVertex(Label label, GdmGraph graph, Action<IOGraphGdmVertexDescriptor<T>> configure)
        : this(configure)
    {
        Label = label;
        Graph = graph;
    }

    #endregion

    #region Properties 

    public Label Label { get; internal set; }
    public IOGraphGdmType Type { get; internal set; } = default!;
    public IOGraphGdmEdgeCollection Edges { get; } = new GdmEdgeCollection();
    public IOGraphGdmOperationCollection Operations { get; } = new GdmVertexOperationCollection();
    public IOGraphGdmMetadata Meta { get; } = new GdmMetadata();
    public IOGraphGdmGraph Graph { get; internal set; } = default!;
    public GdmElementKind ElementKind => GdmElementKind.Vertex;

    #endregion

    #region Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    protected virtual void Configure(IOGraphGdmVertexDescriptor<T> descriptor)
    {
        configure?.Invoke(descriptor);
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

    #region Helpers

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

    #endregion
}