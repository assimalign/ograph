using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm: {Label}")]
internal class Gdm : IOGraphGdm
{
    public Gdm() { }

    public Label Label { get; set; }
    public GdmTypeCollection Types { get; } = new();
    public GdmEdgeCollection Edges { get; } = new();
    public GdmVertexCollection Vertices { get; } = new();
    IOGraphGdmTypeCollection IOGraphGdm.Types => Types;
    IOGraphGdmEdgeCollection IOGraphGdm.Edges => Edges;
    IOGraphGdmVertexCollection IOGraphGdm.Vertices => Vertices;
}
