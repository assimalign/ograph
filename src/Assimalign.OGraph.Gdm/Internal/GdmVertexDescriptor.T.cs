using System;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmVertexDescriptor<T> : IOGraphGdmVertexDescriptor<T>
    where T : class, new()
{
    private readonly GdmVertex<T> vertex;

    public GdmVertexDescriptor(GdmVertex<T> vertex)
    {
        this.vertex = vertex;
    }

    public IOGraphGdmVertexDescriptor<T> HasEdge<TTarget>(Action<IOGraphGdmEdgeDescriptor<T, TTarget>> configure) where TTarget : class, new()
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexDescriptor<T> HasLabel(Label label)
    {
        vertex.label = label;
        return this;
    }

    public IOGraphGdmVertexDescriptor<T> HasType(Action<IOGraphGdmEntityTypeDescriptor<T>> configure)
    {
        vertex.type = new GdmTypeReference()
        {
            Definition = GdmEntityType<T>.Create(configure)
        };
        return this;
    }

    public IOGraphGdmVertexDescriptor<T> HasType(IOGraphGdmEntityType type)
    {
        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }
        if (type.RuntimeType is null)
        {
            throw new ArgumentException("The provided GDM Type has not runtime type");
        }
        if (!type.RuntimeType.IsAssignableTo((typeof(T))))
        {
            throw new InvalidOperationException($"The underlying runtime type: {type.RuntimeType.Name} is not assignable to {typeof(T).Name}.");
        }

        vertex.type = new GdmTypeReference()
        {
            Definition = type
        };

        return this;
    }

    public IOGraphGdmVertexDescriptor<T> HasType<TGdmType>() where TGdmType : IOGraphGdmEntityType, new()
    {
        return HasType(new TGdmType());
    }
}
