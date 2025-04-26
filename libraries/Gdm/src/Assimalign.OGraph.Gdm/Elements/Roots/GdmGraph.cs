using System;

namespace Assimalign.OGraph.Gdm.Elements;

using Assimalign.OGraph.Gdm.Internal;

public class GdmGraph : IOGraphGdmGraph
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
    public GdmTypeCollection Types = new GdmTypeCollection();
    public GdmMetadata Meta { get; } = new GdmMetadata();
    public GdmElementKind ElementKind { get; } = GdmElementKind.Graph;


    IOGraphGdmEdgeCollection IOGraphGdmGraph.Edges => Edges;
    IOGraphGdmVertexCollection IOGraphGdmGraph.Vertices => Vertices;
    IOGraphGdmTypeCollection IOGraphGdmGraph.Types => Types;
    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => Meta;
    IOGraphGdm IOGraphGdmGraph.Model => Model;  


    public static IOGraphGdmGraph Create(GdmName name, Gdm model, Action<IOGraphGdmGraphDescriptor> configure)
    {
        var graph = new GdmGraph(name, model);

        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        var descriptor = new GdmGraphDescriptor(graph);

        configure.Invoke(descriptor);

        return graph;
    }
}
