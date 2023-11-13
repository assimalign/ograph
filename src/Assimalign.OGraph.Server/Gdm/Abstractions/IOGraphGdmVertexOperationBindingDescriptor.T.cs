using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmVertexOperationBindingDescriptor<T>
{
    IOGraphGdmVertexOperationDescriptor HasGet(Label label);
    IOGraphGdmVertexOperationDescriptor HasPut(Label label);
    IOGraphGdmVertexOperationDescriptor HasPost(Label label);
    IOGraphGdmVertexOperationDescriptor HasPatch(Label label);
    IOGraphGdmVertexOperationDescriptor HasDelete(Label label);
}