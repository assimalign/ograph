namespace Assimalign.OGraph.Gdm.Internal;

internal class Gdm : IOGraphGdm
{
    public Gdm()
    {
        Types = new GdmTypeCollection();
        Edges = new GdmEdgeCollection();
        Vertices = new GdmVertexCollection();
    }

    public Label Label { get; set; }
    public IOGraphGdmTypeCollection Types { get; }
    public IOGraphGdmEdgeCollection Edges { get; }
    public IOGraphGdmVertexCollection Vertices { get; }
}
