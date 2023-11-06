using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmVertexDescriptor<T> 
    where T : class, new()
{
    IOGraphGdmVertexDescriptor<T> HasLabel(Label label);
    IOGraphGdmVertexDescriptor<T> HasType(Action<IOGraphGdmEntityTypeDescriptor<T>> configure);

}
