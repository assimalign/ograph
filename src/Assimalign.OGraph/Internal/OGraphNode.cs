using System;

namespace Assimalign.OGraph.Internal;

internal class OGraphNode : IOGraphNode
{
    private readonly IOGraphEdgeCollection edges;
    private readonly IOGraphPropertyCollection properties;
    private readonly IOGraphMetadata metadata;

    public OGraphNode()
    {
        this.edges = new OGraphEdgeCollection();
        this.properties = new OGraphPropertyCollection();
        this.metadata = new OGraphMetadata();
    }

    /// <inheritdoc />
    public Label Label { get; set; }

    /// <inheritdoc />
    public IOGraphType Type { get; set; }

    /// <inheritdoc />
    public IOGraphEdgeCollection Edges => this.edges;

    /// <inheritdoc />
    public IOGraphPropertyCollection Properties => this.properties;

    /// <inheritdoc />
    public IOGraphMetadata Metadata => this.metadata;


    public Action OnEdgeAdd { get; set; } = () => { };
}