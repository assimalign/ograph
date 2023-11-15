using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmOperationBindingDescriptor
{
    IOGraphGdmOperationBindingDescriptor UseRoute(Route route);
    IOGraphGdmOperationBindingDescriptor UseMiddleware<TMiddleware>()
        where TMiddleware : IOGraphGdmOperationBindingMiddleware, new();
    IOGraphGdmOperationBindingDescriptor UseMiddleware(OGraphGdmVertexOperationBindingMiddleware middleware);
    IOGraphGdmOperationBindingDescriptor UseMiddleware(IOGraphGdmOperationBindingMiddleware middleware);
    IOGraphGdmOperationBindingDescriptor UseResolver<TResolver>(TResolver resolver)
        where TResolver : IOGraphGdmOperationBindingResolver, new();
    IOGraphGdmOperationBindingDescriptor UseResolver(OGraphGdmVertexOperationBindingResolver resolver);
    IOGraphGdmOperationBindingDescriptor UseResolver(IOGraphGdmOperationBindingResolver resolver);

}
