using System;
using System.Linq.Expressions;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmBuilder
{
    IOGraphGdmBuilder AddVertex(IOGraphGdmVertex vertex);
    IOGraphGdmBuilder AddVertex<TVertex>() where TVertex : IOGraphGdmVertex, new();
    IOGraphGdmBuilder AddVertex<T>(Action<IOGraphGdmVertexEntityDescriptor<T>> configure)  where T : class, new();
    //IOGraphGdmBuilder AddType<T>(Label label, Action<IOGraphGdmComplexTypeDescriptor<T>> configure) where T : class;
    IOGraphGdm Build();
}


