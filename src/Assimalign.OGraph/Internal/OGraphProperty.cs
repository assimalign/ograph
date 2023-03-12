using System;

namespace Assimalign.OGraph.Internal;

internal class OGraphProperty : IOGraphProperty
{ 
    private readonly IOGraphMetadata metadata;
    private readonly IOGraphPropertyMiddlewareQueue middleware;

    public OGraphProperty()
    {
        this.metadata = new OGraphMetadata();
        this.middleware = new OGraphPropertyMiddlewareQueue();
    }


    public Name Name { get; set; }
    public IOGraphType? Type { get; set; } 
    public IOGraphMetadata Metadata => this.metadata;
    public IOGraphPropertyResolver Resolver { get; set; } = new OGraphPropertyResolverDefault((context) => throw new Exception());
    public IOGraphPropertyMiddlewareQueue Middleware => this.middleware;
    public Type? RuntimeType { get; set; }
    public string? RuntimeName { get; set; }
    public bool IsNullable { get; set; }
    public bool IsFilterable { get; set; }
    public bool IsPagable { get; set; }
    public bool IsSortable { get; set; }
}
