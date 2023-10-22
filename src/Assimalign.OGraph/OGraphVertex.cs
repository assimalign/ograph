using System;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;

public abstract class OGraphVertex : IOGraphVertex
{
    internal Name[] labels;
    internal IOGraphType? type;
    internal IOGraphMetadata metadata;
    internal IOGraphEdgeCollection edges;
    internal IOGraphOperationCollection operations;

    public OGraphVertex()
    {
        this.labels = new Name[0];
        this.metadata = new Metadata();
        this.edges = new OGraphEdgeCollection();
        this.operations = new OGraphOperationCollection();

        Configure(new VertexDescriptor(this));
    }

    Name[] IOGraphVertex.Labels => this.labels;
    IOGraphType IOGraphVertex.Type => this.type!;
    IOGraphMetadata IOGraphVertex.Metadata => this.metadata;
    IOGraphEdgeCollection IOGraphVertex.Edges => this.edges;
    IOGraphOperationCollection IOGraphVertex.Operations => this.operations;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    protected virtual void Configure(IOGraphVertexDescriptor descriptor) { }



    public static IOGraphVertex Create<T>(Action<IOGraphVertexDescriptor<T>> descriptor)
        where T : class, new()
    {
        var vertex = new Default<T>(descriptor);

        vertex.Configure(default);

        return vertex;
    }

    private partial class Default<T> : OGraphVertex<T> where T : class, new()
    {
        private readonly Action<IOGraphVertexDescriptor<T>> configure;

        public Default(Action<IOGraphVertexDescriptor<T>> configure)
        {
            this.configure = configure;
        }
        protected override void Configure(IOGraphVertexDescriptor<T> descriptor)
        {
            configure.Invoke(descriptor);
        }
    }
}