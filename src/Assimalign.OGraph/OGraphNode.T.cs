using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;


/// <inheritdoc />
public abstract class OGraphNode<T> : IOGraphNode
{
    private static readonly ConcurrentDictionary<Type, string> cache = new();

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
    public Label Label { get; set;  }

    /// <inheritdoc />
    public IOGraphEdgeCollection Edges => this.edges;

    /// <inheritdoc />
    public IOGraphPropertyCollection Properties => this.properties;

    /// <inheritdoc />
    public IOGraphMetadata Metadata => this.metadata;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    protected abstract void Configure(IOGraphNodeDescriptor<T> descriptor);


    private void Initialize()
    {
        Configure(new OGraphNodeDescriptor<T>(this));
    }
}
