using System;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm;

using Elements;
using Internal;
using System.Collections.Generic;
using System.Linq;

public class GdmVertexDescriptor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T> : IOGraphGdmNodeDescriptor
{
    private readonly GdmNode<T> _vertex;


    internal GdmVertexDescriptor(GdmNode<T> vertex)
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



    IOGraphGdmNodeDescriptor IOGraphGdmNodeDescriptor.HasLabel(GdmLabel label)
    {
        return HasLabel(label);
    }
    IOGraphGdmNodeDescriptor IOGraphGdmNodeDescriptor.HasEntityType(IOGraphGdmEntityType type)
    {
        return HasEntityType(ThrowHelper.ThrowIfNotType<GdmEntityType<T>>(type));
    }
    IOGraphGdmNodeDescriptor IOGraphGdmNodeDescriptor.HasOperation(IOGraphGdmOperation operation)
    {
        throw new NotImplementedException();
    }

    IOGraphGdmNodeDescriptor IOGraphGdmNodeDescriptor.AddMeta(string key, string value)
    {
        throw new NotImplementedException();
    }
}
