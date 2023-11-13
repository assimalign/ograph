using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmVertexOperationBindingDescriptor
{
    IOGraphGdmVertexOperationBindingDescriptor UseMiddleware(OGraphGdmVertexOperationBindingMiddleware middleware);
    IOGraphGdmVertexOperationBindingDescriptor UseResolver(OGraphGdmVertexOperationBindingResolver resolver);

}
