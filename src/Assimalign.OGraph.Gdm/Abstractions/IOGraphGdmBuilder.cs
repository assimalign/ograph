using System;
using System.Linq.Expressions;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmBuilder
{
    IOGraphGdmBuilder AddType(IOGraphGdmType type);
    IOGraphGdmBuilder AddType<TGdmType>() where TGdmType : class, IOGraphGdmType, new();
    IOGraphGdmBuilder AddType<T>(Action<IOGraphGdmComplexTypeDescriptor<T>> configure) where T : class, new();
    IOGraphGdmBuilder AddVertex(IOGraphGdmVertex vertex);
    IOGraphGdmBuilder AddVertex<TVertex>() where TVertex : IOGraphGdmVertex, new();
    IOGraphGdmBuilder AddVertex<T>(Action<IOGraphGdmEntityTypeDescriptor<T>> configure)  where T : class, new();
    IOGraphGdmBuilder AddVertex<T>(Label label, Action<IOGraphGdmEntityTypeDescriptor<T>> configure) where T : class, new();
    IOGraphGdmBuilder AddVertex(Action<IOGraphGdmVertexDescriptor> configure);
    IOGraphGdmBuilder AddVertex<T>(Action<IOGraphGdmVertexDescriptor<T>> configure) where T : class, new();
    IOGraphGdm Build();
}