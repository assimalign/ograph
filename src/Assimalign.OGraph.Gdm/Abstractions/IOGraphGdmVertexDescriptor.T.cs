using System;
using System.Linq.Expressions;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmVertexDescriptor<T>
{
    IOGraphGdmVertexDescriptor<T> HasIdentifier(Label label);
    IOGraphGdmVertexDescriptor<T> HasIdentifier<TMember>(Expression<Func<T, TMember>> expression) where TMember : struct;
    IOGraphGdmVertexDescriptor<T> HasKey<TMember>(Expression<Func<T, TMember?>> expression) where TMember : struct;
    IOGraphGdmVertexDescriptor<T> HasLabel(Label label);
    IOGraphGdmVertexDescriptor<T> Ignore(Label label);
    IOGraphGdmVertexDescriptor<T> Ignore<TMember>(Expression<Func<T, TMember>> expression);
    IOGraphGdmVertexDescriptor<T> HasProperty(Label label, Action<IOGraphGdmComplexTypeDescriptor> configure);
    IOGraphGdmVertexDescriptor<T> HasProperty<TMember>(Expression<Func<T, TMember?>> expression, Action<IOGraphGdmComplexTypeDescriptor<TMember?>> configure) where TMember : class;
    IOGraphGdmPropertyDescriptor<T> HasProperty<TMember>(Expression<Func<T, TMember>> expression) where TMember : struct;
    IOGraphGdmPropertyDescriptor<T> HasProperty<TMember>(Expression<Func<T, TMember?>> expression) where TMember : struct;
    IOGraphGdmEdgeDescriptor HasEdge(Label label);
    IOGraphGdmEdgeDescriptor<TVertex> HasEdge<TVertex>(Label label) where TVertex : class, new();
}
