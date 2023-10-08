namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;

public abstract class OGraphNode : IOGraphNode
{
    internal Name label;
    internal IOGraphType? type;
    internal IOGraphMetadata metadata;
    internal IOGraphEdgeCollection edges;
    internal IOGraphOperationCollection operations;

    public OGraphNode()
    {
        this.metadata = new OGraphMetadata();
        this.edges = new OGraphEdgeCollection();
        this.operations = new OGraphOperationCollection();

        Configure(new OGraphNodeDescriptor(this));
    }

    /// <inheritdoc />
    Name IOGraphNode.Label => this.label;

    /// <inheritdoc />
    IOGraphType IOGraphNode.Type => this.type!;

    /// <inheritdoc />
    IOGraphMetadata IOGraphNode.Metadata => this.metadata;

    /// <inheritdoc />
    IOGraphEdgeCollection IOGraphNode.Edges => this.edges;

    IOGraphOperationCollection IOGraphNode.Operations => this.operations;

    protected virtual void Configure(IOGraphNodeDescriptor descriptor) { }
}