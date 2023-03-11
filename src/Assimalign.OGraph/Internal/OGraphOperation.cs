using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphOperation : IOGraphOperation
{
    private readonly IOGraphOperationMiddlewareQueue middleware;

    public OGraphOperation()
    {
        this.middleware = new OGraphOperationMiddlewareQueue();
    }

    public Name Name { get; set; }
    public Route Route { get; set; }
    public Method Method { get; set; }
    public bool IsEnabled { get; set; }
    public IOGraphType? RequestType { get; set; }
    public IOGraphType? ResponseType { get; set; }
    public IOGraphNode? Node { get; set; }
    public IOGraphOperationResolver? Resolver { get; set; }
    public IOGraphOperationMiddlewareQueue Middleware => this.middleware;
}
