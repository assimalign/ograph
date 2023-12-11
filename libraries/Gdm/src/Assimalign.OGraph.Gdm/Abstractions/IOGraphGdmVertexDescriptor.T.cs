using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmVertexDescriptor<T> 
    where T : class, new()
{
    IOGraphGdmVertexDescriptor<T> HasLabel(Label label);
    IOGraphGdmVertexDescriptor<T> HasType<TGdmType>() where TGdmType : IOGraphGdmEntityType, new();
    IOGraphGdmVertexDescriptor<T> HasType(IOGraphGdmEntityType type);
    IOGraphGdmVertexDescriptor<T> HasEdge<TTarget>(Action<IOGraphGdmEdgeDescriptor<T, TTarget>> configure) where TTarget : class, new();
}
