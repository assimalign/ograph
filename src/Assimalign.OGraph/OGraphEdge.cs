using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public abstract class OGraphEdge : IOGraphEdge
{
    public Name Name => throw new NotImplementedException();

    public IOGraphNode Source => throw new NotImplementedException();

    public IOGraphNode Target => throw new NotImplementedException();

    public IOGraphMetadata Metadata => throw new NotImplementedException();

    public IOGraphEdgeResolver Resolver => throw new NotImplementedException();

    public IOGraphEdgeMiddlewareQueue Middleware => throw new NotImplementedException();
}
