namespace Assimalign.OGraph.Internal;

internal class OGraphEdge : IOGraphEdge
{
    public OGraphEdge()
    {
        this.Metadata = new OGraphMetadata();
        this.Middleware = new OGraphEdgeMiddlewareQueue();
    }

    public Name Name { get; set; }
    public IOGraphNode Source { get; set; }
    public IOGraphNode Target { get; set; }
    public IOGraphMetadata Metadata { get; }
    public IOGraphEdgeResolver Resolver { get; set; }
    public IOGraphEdgeMiddlewareQueue Middleware { get; }
}
