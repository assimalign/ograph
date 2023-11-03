using System;
using System.Linq.Expressions;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmVertexEntityDescriptor<T>
{
    IOGraphGdmVertexEntityDescriptor<T> HasKey(Label label);
    IOGraphGdmVertexEntityDescriptor<T> HasKey<TMember>(Expression<Func<T, TMember>> expression) 
        where TMember : struct;
    IOGraphGdmVertexEntityDescriptor<T> HasKey<TMember>(Expression<Func<T, TMember?>> expression) 
        where TMember : struct;

    IOGraphGdmVertexEntityDescriptor<T> HasLabel(Label label);
    IOGraphGdmVertexEntityDescriptor<T> Ignore(Label label);
    IOGraphGdmVertexEntityDescriptor<T> Ignore<TMember>(Expression<Func<T, TMember>> expression);
    IOGraphGdmVertexEntityDescriptor<T> HasProperty(Label label, Action<IOGraphGdmComplexTypeDescriptor> configure);
    //IOGraphGdmVertexEntityDescriptor<T> HasProperty<TMember>(Expression<Func<T, TMember?>> expression, Action<IOGraphGdmComplexTypeDescriptor<TMember?>> configure) where TMember : class;
    IOGraphGdmVertexEntityDescriptor<T> HasMetadata(Label label, MetaValue value);

    IOGraphGdmPropertyDescriptor<TMember?> HasProperty<TMember>(Expression<Func<T, TMember?>> expression);
    

    IOGraphGdmEdgeDescriptor HasEdge(Label label);
    IOGraphGdmEdgeDescriptor<TVertex> HasEdge<TVertex>(Label label) where TVertex : class, new();
}
