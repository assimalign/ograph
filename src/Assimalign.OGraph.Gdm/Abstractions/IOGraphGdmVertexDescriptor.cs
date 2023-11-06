using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmVertexDescriptor
{
    IOGraphGdmVertexDescriptor HasLabel(Label label);
    IOGraphGdmVertexDescriptor HasType(Type type);
    IOGraphGdmVertexDescriptor HasType<T>() where T : class, new();
    IOGraphGdmVertexDescriptor HasType<T>(Action<IOGraphGdmEntityTypeDescriptor<T>> configure) where T : class, new();

}
