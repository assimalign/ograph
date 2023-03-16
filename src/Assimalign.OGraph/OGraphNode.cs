using System;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;

public abstract class OGraphNode : IOGraphNode
{
    internal Label label;
    internal IOGraphType? type;
    internal IOGraphMetadata metadata;
    internal IOGraphEdgeCollection edges;

    public OGraphNode()
    {
        this.metadata = new OGraphMetadata();
        this.edges = new OGraphEdgeCollection();
    }

    /// <inheritdoc />
    Label IOGraphNode.Label => this.label;

    /// <inheritdoc />
    IOGraphType? IOGraphNode.Type => this.type;

    /// <inheritdoc />
    IOGraphMetadata IOGraphNode.Metadata => this.metadata;

    /// <inheritdoc />
    IOGraphEdgeCollection IOGraphNode.Edges => this.edges;


   



    internal bool IsValid(out Exception? exception)
    {
        exception = default;






        return true;
    }

}




public abstract class OGraphNode<TType> : OGraphNode 
    where TType :  IOGraphType, new()
{
    public OGraphNode()
    {
        base.type = new TType();
        base.label = new Label(type.TypeName);
    }

    protected abstract void Configure(IOGraphNodeDescriptor descriptor);
}