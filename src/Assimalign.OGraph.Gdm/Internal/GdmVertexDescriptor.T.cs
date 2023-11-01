using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmVertexDescriptor<T> : IOGraphGdmVertexDescriptor<T> where T : class
{
    private readonly GdmVertex vertex;

    public GdmVertexDescriptor(GdmVertex vertex)
    {
        this.vertex = vertex;
    }
    public IOGraphGdmEdgeDescriptor HasEdge(Label label)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmEdgeDescriptor<TVertex> HasEdge<TVertex>(Label label) where TVertex : class, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexDescriptor<T> HasIdentifier(Label label)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexDescriptor<T> HasIdentifier<TMember>(System.Linq.Expressions.Expression<Func<T, TMember>> expression) where TMember : struct
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexDescriptor<T> HasKey<TMember>(System.Linq.Expressions.Expression<Func<T, TMember?>> expression) where TMember : struct
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexDescriptor<T> HasLabel(Label label)
    {
        throw new NotImplementedException();
    }
    public IOGraphGdmVertexDescriptor<T> HasProperty(Label label, Action<IOGraphGdmComplexTypeDescriptor> configure)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexDescriptor<T> HasProperty<TMember>(System.Linq.Expressions.Expression<Func<T, TMember?>> expression, Action<IOGraphGdmComplexTypeDescriptor<TMember?>> configure) where TMember : class
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmPropertyDescriptor<T> HasProperty<TMember>(System.Linq.Expressions.Expression<Func<T, TMember>> expression) where TMember : struct
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmPropertyDescriptor<T> HasProperty<TMember>(System.Linq.Expressions.Expression<Func<T, TMember?>> expression) where TMember : struct
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexDescriptor<T> Ignore(Label label)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexDescriptor<T> Ignore<TMember>(System.Linq.Expressions.Expression<Func<T, TMember>> expression)
    {
        throw new NotImplementedException();
    }
}
