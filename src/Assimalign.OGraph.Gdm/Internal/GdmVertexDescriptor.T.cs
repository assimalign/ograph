using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmVertexDescriptor<T> : IOGraphGdmVertexDescriptor<T>
    where T : class, new()
{
    private readonly GdmVertex<T> vertex;

    public GdmVertexDescriptor(GdmVertex<T> vertex)
    {
        this.vertex = vertex;
    }

    public IOGraphGdmVertexDescriptor<T> HasLabel(Label label)
    {
        vertex.label = label;
        return this;
    }

    public IOGraphGdmVertexDescriptor<T> HasType(Action<IOGraphGdmEntityTypeDescriptor<T>> configure)
    {
        throw new NotImplementedException();
    }
}
