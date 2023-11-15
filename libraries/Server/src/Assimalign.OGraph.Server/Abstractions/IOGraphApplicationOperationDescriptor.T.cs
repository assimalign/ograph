using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;

public interface IOGraphApplicationOperationDescriptor<T> where T : class, new()
{
    IOGraphGdmPropertyBindingDescriptor MapGet(Label operationName);
    IOGraphGdmPropertyBindingDescriptor MapPut(Label operationName);
    IOGraphGdmPropertyBindingDescriptor MapPost(Label operationName);
    IOGraphGdmPropertyBindingDescriptor MapPatch(Label operationName);
    IOGraphGdmPropertyBindingDescriptor MapDelete(Label operationName);
}
