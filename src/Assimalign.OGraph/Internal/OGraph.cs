using System;

namespace Assimalign.OGraph.Internal;

internal class OGraph
    : IOGraph
{
    private readonly OGraphOptions options;
    public OGraph(OGraphOptions options)
    {
        this.options = options;
    }

    public Label Label { get; set; }

    public VertexCollection Nodes { get; } = new VertexCollection();
    IOGraphVertexCollection IOGraph.Vertices => this.Nodes;
    
    public EdgeCollection Edges { get; } = new EdgeCollection();
    IOGraphEdgeCollection IOGraph.Edges => this.Edges;

    public OGraphOperationCollection Operations { get; } = new OGraphOperationCollection();
    IOGraphOperationCollection IOGraph.Operations => this.Operations;

    public TypeCollection Types { get; } = new TypeCollection();
    IOGraphTypeCollection IOGraph.Types => this.Types;

    public IOGraphExecutor GetExecutor()
    {
        throw new NotImplementedException();
    }
}
