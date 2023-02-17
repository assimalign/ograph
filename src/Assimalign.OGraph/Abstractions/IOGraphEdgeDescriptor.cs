using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphEdgeDescriptor
{
    IOGraphEdgeDescriptor UseMiddleware(IOGraphEdgeMiddleware middleware);
}

public interface IOGraphEdgeDescriptor<T>
{
    IOGraphEdgeDescriptor<T> UseMiddleware(IOGraphEdgeMiddleware middleware);
    IOGraphEdgeDescriptor<T> UseResolver(OGraphEdgeResolver<T> resolver);
}
