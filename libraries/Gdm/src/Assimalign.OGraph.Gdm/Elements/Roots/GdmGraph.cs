using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Elements;

using Assimalign.OGraph.Gdm.Internal;

public class GdmGraph : GdmElement, IOGraphGdmGraph
{
    public GdmGraph(GdmName name, Gdm model)
    {
        Name = name;
        Model = model;
    }

    public GdmName Name { get; }
    public Gdm Model { get; }
    public GdmEdgeCollection Edges { get; } = new GdmEdgeCollection();
    public GdmVertexCollection Vertices { get; } = new GdmVertexCollection();
    public GdmTypeCollection Types { get; } = new GdmTypeCollection();
    public override GdmElementKind ElementKind { get; } = GdmElementKind.Graph;
    public override IEnumerable<TElement> GetElements<TElement>()
    {
        if (this is TElement element)
        {
            yield return element;
        }

        foreach (var type in GetElements<TElement, GdmType>(Types))
        {
            yield return type;
        }

        //foreach (var vertex in GetElements<TElement, >)
    }

    private IEnumerable<T> GetElements<T, TAny>(IEnumerable<TAny> enumerable) where TAny : GdmElement
    {
        return enumerable.SelectMany(p => p.GetElements<T>());
    }

    public static GdmGraph Create(GdmName name, Gdm model, Action<GdmGraphDescriptor> configure)
    {
        var graph = new GdmGraph(name, model);
        var descriptor = new GdmGraphDescriptor(graph);

        ThrowHelper.ThrowIfNull(configure).Invoke(descriptor);

        return graph;
    }

    IOGraphGdmEdgeCollection IOGraphGdmGraph.Edges => Edges;
    IOGraphGdmVertexCollection IOGraphGdmGraph.Vertices => Vertices;
    IOGraphGdmTypeCollection IOGraphGdmGraph.Types => Types;
    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => Meta;
    IOGraphGdm IOGraphGdmGraph.Model => Model;  
}
