using System;

namespace Assimalign.OGraph.Internal;

internal class Graph : IOGraph
{
    public Graph()
    {
        Edges = new OGraphEdgeCollection();
        Nodes = new VertexCollection();
        Operations = new OGraphOperationCollection();
        Types = new TypeCollection();
    }


    public Name Label { get; set; }

    public VertexCollection Nodes { get; }
    IOGraphVertexCollection IOGraph.Vertices => this.Nodes;
    
    public OGraphEdgeCollection Edges { get; }
    IOGraphEdgeCollection IOGraph.Edges => this.Edges;

    public OGraphOperationCollection Operations { get; }
    IOGraphOperationCollection IOGraph.Operations => this.Operations;

    public TypeCollection Types { get; }
    IOGraphTypeCollection IOGraph.Types => this.Types;

    
}
