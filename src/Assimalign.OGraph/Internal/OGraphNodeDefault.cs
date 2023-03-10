using System;

namespace Assimalign.OGraph.Internal;

internal class OGraphNodeDefault<T> : IOGraphNode
{
    private readonly IOGraphEdgeCollection edges;
    private readonly IOGraphPropertyCollection properties;
    private readonly IOGraphMetadata metadata;

    private readonly Action<IOGraphNodeDescriptor<T>> configure;
    public OGraphNodeDefault(Action<IOGraphNodeDescriptor<T>> configure)
    {
        this.edges = new OGraphEdgeCollection();
        this.properties = new OGraphPropertyCollection();
        this.metadata = new OGraphMetadata();
        this.configure = configure;

        Configure(new OGraphNodeDescriptor<T>(this));
    }

    /// <inheritdoc />
    public Label Label { get; set; }

    /// <inheritdoc />
    public IOGraphEdgeCollection Edges => this.edges;

    /// <inheritdoc />
    public IOGraphPropertyCollection Properties => this.properties;

    /// <inheritdoc />
    public IOGraphMetadata Metadata => this.metadata;

    private void Configure(IOGraphNodeDescriptor<T> descriptor)
    {
        if (configure is null)
        {
            throw new ArgumentNullException(nameof(configure));
        }

        configure.Invoke(descriptor);

    }


}
