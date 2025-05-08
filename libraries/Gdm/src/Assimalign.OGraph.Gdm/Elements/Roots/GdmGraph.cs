using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

/// <summary>
/// 
/// </summary>
public sealed class GdmGraph : GdmNamedElement, IOGraphGdmGraph
{
    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="model"></param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public GdmGraph(GdmName name, Gdm model) : base(name)
    {
        Model = ThrowHelper.ThrowIfNull(model);
    }

    #endregion

    #region Properties

    public Gdm Model { get; }
    public GdmEdgeCollection Edges { get; } = new GdmEdgeCollection();
    public GdmVertexCollection Vertices { get; } = new GdmVertexCollection();
    public GdmTypeCollection Types { get; } = new GdmTypeCollection();
    public sealed override GdmElementKind ElementKind { get; } = GdmElementKind.Graph;
    IOGraphGdmEdgeCollection IOGraphGdmGraph.Edges => Edges;
    IOGraphGdmVertexCollection IOGraphGdmGraph.Vertices => Vertices;
    IOGraphGdmTypeCollection IOGraphGdmGraph.Types => Types;
    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => Meta;
    IOGraphGdm IOGraphGdmGraph.Model => Model;

    #endregion

    #region Methods

    public sealed override IEnumerable<TElement> GetElements<TElement>()
    {
        if (this is TElement element)
        {
            yield return element;
        }
        foreach (var type in GetElements<TElement, GdmType>(Types))
        {
            yield return type;
        }
        foreach (var vertex in GetElements<TElement, GdmVertex>(Vertices))
        {
            yield return vertex;
        }
        foreach (var edge in GetElements<TElement, GdmEdge>(Edges))
        {
            yield return edge;
        }
    }

    private IEnumerable<T> GetElements<T, TAny>(IEnumerable<TAny> enumerable) where TAny : GdmElement
    {
        return enumerable.SelectMany(p => p.GetElements<T>());
    }

    public static GdmGraph Create(GdmName name, Gdm model, Action<GdmGraphDescriptor> configure)
    {
        var descriptor = new GdmGraphDescriptor(new GdmGraph(name, model));

        ThrowHelper.ThrowIfNull(configure).Invoke(descriptor);

        return descriptor.Describe();
    }

    #endregion
}
