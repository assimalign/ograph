using System;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm;

using Elements;
using Internal;
using System.Collections.Generic;
using System.Linq;

public class GdmVertexDescriptor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T> : IOGraphGdmVertexDescriptor
    where T : class, new()
{
    private readonly GdmVertex<T> _vertex;


    internal GdmVertexDescriptor(GdmVertex<T> vertex)
    {
        _vertex = vertex;
    }


   // public GdmVertex<T> Vertex { get; }
    public GdmVertexDescriptor<T> HasLabel(GdmLabel label)
    {
        _vertex.SetLabel(label);
        return this;
    }
    

    public GdmVertexDescriptor<T> HasEntityType(GdmName entityName)
    {
        _vertex.SetEntityType(_vertex.Graph.Types.OfType<GdmEntityType>().Find(entityName)!);
        return this;
    }


    public GdmVertexDescriptor<T> HasEntityType(GdmEntityType<T> entity)
    {
        _vertex.SetEntityType(entity);
        return this;
    }

    public GdmVertexDescriptor<T> HasEntityType(Action<GdmEntityTypeDescriptor<T>> configure)
    {
        var entityType = _vertex.Graph.Types.OfType<GdmEntityType<T>>()
            .FirstOrDefault();

        if (entityType is null)
        {
            entityType = new GdmEntityTypeDefault<T>(_vertex.Graph, configure);
            entityType.Configure();
        }
        else
        {
            var descriptor = new GdmEntityTypeDescriptor<T>(entityType);

            configure.Invoke(descriptor);
        }

        return HasEntityType(entityType);
    }



    IOGraphGdmVertexDescriptor IOGraphGdmVertexDescriptor.HasLabel(GdmLabel label)
    {
        return HasLabel(label);
    }
    IOGraphGdmVertexDescriptor IOGraphGdmVertexDescriptor.HasEntityType(IOGraphGdmEntityType type)
    {
        return HasEntityType(ThrowHelper.ThrowIfNotType<GdmEntityType<T>>(type));
    }
    IOGraphGdmVertexDescriptor IOGraphGdmVertexDescriptor.HasOperation(IOGraphGdmOperation operation)
    {
        throw new NotImplementedException();
    }

    IOGraphGdmVertexDescriptor IOGraphGdmVertexDescriptor.AddMeta(string key, string value)
    {
        throw new NotImplementedException();
    }
}
