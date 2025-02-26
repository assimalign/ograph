using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

[DebuggerDisplay("{Label} [Vertex]")]
public class GdmVertex<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T> : 
    IOGraphGdmVertex 
    where T : class, new()
{
    private readonly Action<IOGraphGdmVertexDescriptor<T>>? configure;

    #region Constructors

    GdmVertex(Action<IOGraphGdmVertexDescriptor<T>> configure)
    {
        this.configure = configure;
        Configure(new GdmVertexDescriptor<T>(this));
    }

    public GdmVertex(GdmLabel label, GdmEntityType<T> type, GdmGraph graph) : this(descriptor =>
    {
        descriptor.HasLabel(label);
        descriptor.HasEntityType(type);
    })
    {
        Graph = ThrowHelper.ThrowIfNull(graph, nameof(graph));
    }

    #endregion

    #region Properties

    public GdmLabel Label { get; internal set; } = typeof(T).Name;
    public GdmEntityType<T> Type { get; internal set; } = default!;
    public GdmGraph Graph { get; internal set; } = default!;
    public GdmEdgeCollection Edges { get; } = new GdmEdgeCollection();
    public GdmVertexOperationCollection Operations { get; } = new GdmVertexOperationCollection();
    public GdmMetadata Meta { get; } = new GdmMetadata();
    public GdmElementKind ElementKind => GdmElementKind.Vertex;
    IOGraphGdmType IOGraphGdmVertex.Type => Type;
    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => Meta;
    IOGraphGdmOperationCollection IOGraphGdmVertex.Operations => Operations;
    IOGraphGdmEdgeCollection IOGraphGdmVertex.Edges => Edges;
    IOGraphGdmGraph IOGraphGdmVertex.Graph => Graph;

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static GdmVertex<T> Create(Action<IOGraphGdmVertexDescriptor<T>> configure)
    {
        return new GdmVertex<T>(configure);
    }
}