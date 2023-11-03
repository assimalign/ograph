using System;

namespace Assimalign.OGraph;

public interface IOGraphModelDescriptor
{
    IOGraphModelDescriptor AddVertex<T>()
        where T : IOGraphVertex, new();
    IOGraphModelDescriptor AddVertex(IOGraphVertex vertex);
    IOGraphModelDescriptor AddVertex(Action<IOGraphModelVertexDescriptor> configure);
    IOGraphModelDescriptor AddVertex<T>(Action<IOGraphModelVertexDescriptor<T>> configure)
        where T : class, new();
    IOGraphModelDescriptor AddType<T>(Label name, Action<IOGraphComplexTypeDescriptor<T>> configure)
        where T : class, new();
}
