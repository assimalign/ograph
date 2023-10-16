using System;

namespace Assimalign.OGraph.Internal;

internal class OGraph : IOGraph
{
    public OGraph()
    {
        Edges = new OGraphEdgeCollection();
        Nodes = new OGraphNodeCollection();
        Operations = new OGraphOperationCollection();
        Types = new OGraphTypeCollection();
    }


    public Name Name { get; set; }

    public OGraphNodeCollection Nodes { get; }
    IOGraphNodeCollection IOGraph.Nodes => this.Nodes;
    
    public OGraphEdgeCollection Edges { get; }
    IOGraphEdgeCollection IOGraph.Edges => this.Edges;

    public OGraphOperationCollection Operations { get; }
    IOGraphOperationCollection IOGraph.Operations => this.Operations;

    public OGraphTypeCollection Types { get; }
    IOGraphTypeCollection IOGraph.Types => this.Types;

    
}
