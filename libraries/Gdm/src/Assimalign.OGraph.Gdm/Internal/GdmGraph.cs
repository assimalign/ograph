using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmGraph : IOGraphGdmGraph
{
    private readonly Dictionary<Label, IOGraphGdmVertex> vertices;
    private readonly Dictionary<Label, IOGraphGdmEdge> edges;

    public GdmGraph(Label label)
    {
        Label = label;
        this.vertices = new();
        this.edges = new();
    }

    public Label Label { get; }
    public IEnumerable<IOGraphGdmVertex> Vertices => vertices.Values;
    public IEnumerable<IOGraphGdmEdge> Edges => edges.Values;
    public IEnumerable<IOGraphGdmBinding> Bindings => throw new NotImplementedException();

    public GdmElementKind ElementKind => GdmElementKind.Graph;

    public void Bind(IOGraphGdmBinding binding)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmEdge GetEdge(Label label) => edges[label];
    public IOGraphGdmVertex GetVertex(Label label) => vertices[label];

    public void Unbind(IOGraphGdmBinding binding)
    {
        throw new NotImplementedException();
    }
}
