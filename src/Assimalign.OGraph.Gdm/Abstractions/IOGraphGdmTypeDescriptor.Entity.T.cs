using System;
using System.Linq.Expressions;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmEntityTypeDescriptor<T> where T : class, new()
{
    IOGraphGdmEntityTypeDescriptor<T> HasKey(Label label);
    IOGraphGdmEntityTypeDescriptor<T> HasKey<TMember>(Expression<Func<T, TMember>> expression) where TMember : struct;
    IOGraphGdmEntityTypeDescriptor<T> HasKey<TMember>(Expression<Func<T, TMember?>> expression) where TMember : struct; 
    IOGraphGdmEntityTypeDescriptor<T> Ignore(Label label);
    IOGraphGdmEntityTypeDescriptor<T> Ignore<TMember>(Expression<Func<T, TMember>> expression);
    IOGraphGdmPropertyDescriptor<TMember?> HasProperty<TMember>(Expression<Func<T, TMember?>> expression);
    
    // IOGraphGdmEntityTypeDescriptor<T> HasProperty(Label label, Action<IOGraphGdmComplexTypeDescriptor> configure);
    //IOGraphGdmEntityTypeDescriptor<T> HasProperty<TMember>(Expression<Func<T, TMember?>> expression, Action<IOGraphGdmComplexTypeDescriptor<TMember?>> configure) where TMember : class, new();
    //IOGraphGdmEdgeDescriptor HasEdge(Label label);
    //IOGraphGdmEdgeDescriptor<TVertex> HasEdge<TVertex>(Label label) where TVertex : class, new();
}
