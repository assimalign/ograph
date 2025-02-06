using System;

namespace Assimalign.OGraph.Gdm.Elements;

using Assimalign.OGraph.Gdm.Internal;

public class GdmGraph : IOGraphGdmGraph
{
    public GdmGraph(Label label)
    {
        Label = label;
    }

    public Label Label { get; }
    public IOGraphGdmMetadata Meta { get; } = new GdmMetadata();
    public IOGraphGdmEdgeCollection Edges { get; } = new GdmEdgeCollection();
    public IOGraphGdmVertexCollection Vertices {get;} = new GdmVertexCollection();
    public IOGraphGdmTypeCollection Types {get;} = new GdmTypeCollection();
    public GdmElementKind ElementKind { get; } = GdmElementKind.Graph;
    public IOGraphGdmEdge GetEdge(Label label)
    {
        return Edges.Find(label);
    }
    public IOGraphGdmType GetType(Label label)
    {
        return Types.Find(label);
    }
    public IOGraphGdmVertex GetVertex(Label label)
    {
        return Vertices.Find(label);
    }
    public static IOGraphGdmGraph Create(Label label, Action<IOGraphGdmGraphDescriptor> configure)
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
