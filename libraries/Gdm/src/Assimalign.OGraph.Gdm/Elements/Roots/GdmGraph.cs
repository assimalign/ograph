using System;

namespace Assimalign.OGraph.Gdm.Elements;

using Assimalign.OGraph.Gdm.Internal;

public class GdmGraph : IOGraphGdmGraph
{
    public GdmGraph(GdmLabel label)
    {
        Label = label;
    }

    public GdmLabel Label { get; }
    public GdmEdgeCollection Edges { get; } = new GdmEdgeCollection();
    public GdmVertexCollection Vertices { get; } = new GdmVertexCollection();
    public GdmTypeCollection Types = new GdmTypeCollection();
    public GdmMetadata Meta { get; } = new GdmMetadata();
    public GdmElementKind ElementKind { get; } = GdmElementKind.Graph;


    IOGraphGdmEdgeCollection IOGraphGdmGraph.Edges => Edges;
    IOGraphGdmVertexCollection IOGraphGdmGraph.Vertices => Vertices;
    IOGraphGdmTypeCollection IOGraphGdmGraph.Types => Types;
    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => Meta;


    public static IOGraphGdmGraph Create(GdmLabel label, Action<IOGraphGdmGraphDescriptor> configure)
    {
        var graph = new GdmGraph(label);

        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        var descriptor = new GdmGraphDescriptor(graph);

        configure.Invoke(descriptor);

        return graph;
    }
}
