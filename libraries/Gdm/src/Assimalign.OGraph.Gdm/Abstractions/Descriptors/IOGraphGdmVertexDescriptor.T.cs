using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmVertexDescriptor<T> 
    where T : class, new()
{
    IOGraphGdmVertexDescriptor<T> HasLabel(Label label);
    IOGraphGdmVertexDescriptor<T> HasEntityType<TType>() where TType : IOGraphGdmEntityType, new();
    IOGraphGdmVertexDescriptor<T> HasEntityType(IOGraphGdmEntityType type);
    IOGraphGdmVertexDescriptor<T> HasEdge<TTarget>(Label label) where TTarget : class, IOGraphGdmVertex, new();
}
