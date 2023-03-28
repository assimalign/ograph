namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;

public abstract class OGraphNode : IOGraphNode
{
    internal Label label;
    internal IOGraphType type;
    internal IOGraphMetadata metadata;
    internal IOGraphEdgeCollection edges;

    public OGraphNode()
    {
        this.metadata = new OGraphMetadata();
        this.edges = new OGraphEdgeCollection();

        Configure(new OGraphNodeDescriptor(this));
    }

    /// <inheritdoc />
    Label IOGraphNode.Label => this.label;

    /// <inheritdoc />
    IOGraphType? IOGraphNode.Type => this.type;

    /// <inheritdoc />
    IOGraphMetadata IOGraphNode.Metadata => this.metadata;

    /// <inheritdoc />
    IOGraphEdgeCollection IOGraphNode.Edges => this.edges;


    protected virtual void Configure(IOGraphNodeDescriptor descriptor) { }
}