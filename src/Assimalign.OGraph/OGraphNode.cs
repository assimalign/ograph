using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;

/// <inheritdoc />
public abstract class OGraphNode : IOGraphNode
{
    private readonly IOGraphEdgeCollection edges;
    private readonly IOGraphPropertyCollection properties;
    private readonly IOGraphMetadata metadata;

    public OGraphNode()
    {
        this.edges = new OGraphEdgeCollection();
        this.properties = new OGraphPropertyCollection();
        this.metadata = new OGraphMetadata();

        Initialize();
    }


    /// <inheritdoc />
    public Label Label { get; set; }

    /// <inheritdoc />
    public IOGraphEdgeCollection Edges => this.edges;

    /// <inheritdoc />
    public IOGraphPropertyCollection Properties => this.properties;

    /// <inheritdoc />
    public IOGraphMetadata Metadata => this.metadata;

    protected abstract void Configure(IOGraphNodeDescriptor descriptor);


    private void Initialize()
    {
        Configure(new OGraphNodeDescriptor(this));
    }
}


