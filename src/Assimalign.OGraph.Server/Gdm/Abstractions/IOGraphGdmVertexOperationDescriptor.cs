using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmVertexOperationDescriptor
{
    IOGraphGdmVertexOperationDescriptor UseRoute(Route route);
    IOGraphGdmVertexOperationDescriptor UseMiddleware<TMiddleware>()
        where TMiddleware : IOGraphGdmVertexOperationBindingMiddleware, new();
    IOGraphGdmVertexOperationDescriptor UseMiddleware(OGraphGdmVertexOperationBindingMiddleware middleware);
    IOGraphGdmVertexOperationDescriptor UseMiddleware(IOGraphGdmVertexOperationBindingMiddleware middleware);
    IOGraphGdmVertexOperationDescriptor UseResolver<TResolver>(TResolver resolver)
        where TResolver : IOGraphGdmVertexOperationBindingResolver, new();
    IOGraphGdmVertexOperationDescriptor UseResolver(OGraphGdmVertexOperationBindingResolver resolver);
    IOGraphGdmVertexOperationDescriptor UseResolver(IOGraphGdmVertexOperationBindingResolver resolver);
}
