using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

/// <summary>
/// 
/// </summary>
public sealed class GdmGraph : GdmElement, IOGraphGdmGraph
{
    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="model"></param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public GdmGraph(GdmDomain domain, Gdm model)
    {
        Domain = domain;
        Model = ThrowHelper.ThrowIfNull(model);
    }

    #endregion

    #region Properties
    public GdmDomain Domain { get; }
    public Gdm Model { get; }
    public GdmEdgeCollection Edges { get; } = new GdmEdgeCollection();
    public GdmNodeCollection Vertices { get; } = new GdmNodeCollection();
    public GdmTypeCollection Types { get; } = new GdmTypeCollection();
    public sealed override GdmElementKind ElementKind { get; } = GdmElementKind.Graph;
    IOGraphGdmEdgeCollection IOGraphGdmGraph.Edges => Edges;
    IOGraphGdmNodeCollection IOGraphGdmGraph.Nodes => Vertices;
    IOGraphGdmTypeCollection IOGraphGdmGraph.Types => Types;
    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => Meta;
    IOGraphGdm IOGraphGdmGraph.Model => Model;
    // TODO: [O01.01.02.02] type-system runtime
    IOGraphGdmEventCollection IOGraphGdmGraph.Events => throw new NotImplementedException();
    // TODO: [O01.01.02.02] type-system runtime
    IOGraphGdmSubscriberCollection IOGraphGdmGraph.Subscribers => throw new NotImplementedException();

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
        foreach (var vertex in GetElements<TElement, GdmNode>(Vertices))
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

    public static GdmGraph Create(GdmDomain domain, Gdm model, Action<GdmGraphDescriptor> configure)
    {
        var descriptor = new GdmGraphDescriptor(new GdmGraph(domain, model));

        ThrowHelper.ThrowIfNull(configure).Invoke(descriptor);

        return descriptor.Describe();
    }

    #endregion
}
