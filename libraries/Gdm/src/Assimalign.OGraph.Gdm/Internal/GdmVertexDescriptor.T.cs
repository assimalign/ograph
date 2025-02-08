using System;

namespace Assimalign.OGraph.Gdm.Internal;

using Assimalign.OGraph.Gdm.Elements;

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
    public IOGraphGdmVertexDescriptor<T> HasEntityType(IOGraphGdmEntityType type)
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

    public IOGraphGdmVertexDescriptor<T> HasEntityType<TGdmType>() where TGdmType : IOGraphGdmEntityType, new()
    {
        return HasEntityType(new TGdmType());
    }

    IOGraphGdmVertexDescriptor<T> IOGraphGdmVertexDescriptor<T>.HasEdge<TTarget>(Label label)
    {
        return this;
    }

    public IOGraphGdmVertexDescriptor<T> AddMeta(string key, string value)
    {
        throw new NotImplementedException();
    }
}
