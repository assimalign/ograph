using System;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm;

using Elements;
using Internal;
using System.Collections.Generic;

public class GdmVertexDescriptor<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T> : IOGraphGdmVertexDescriptor
    where T : class, new()
{
    private readonly GdmGraph _graph;


    internal GdmVertexDescriptor(GdmGraphDescriptor parent)
    {
        Parent = parent;
        //_vertex = vertex;
    }

    internal GdmGraphDescriptor Parent { get; }


   // public GdmVertex<T> Vertex { get; }
    public GdmVertexDescriptor<T> HasLabel(GdmLabel label)
    {
       // Vertex.Label = label;
        return this;
    }
    

    public GdmVertexDescriptor<T> HasEntityType(GdmName entityName)
    {
        return this;
    }


    public GdmVertexDescriptor<T> HasEntityType(GdmEntityType<T> entity)
    {
        _parent.AddType(entity);
       // _vertex.Type = ThrowHelper.ThrowIfNull(entity);
        return this;
    }

    public GdmVertexDescriptor<T> HasEntityType(Action<GdmEntityTypeDescriptor<T>> configure)
    {
        _parent.AddEntityType(configure);
        return this;
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

    IOGraphGdmVertex IOGraphGdmDescriptor<IOGraphGdmVertex>.Describe()
    {
        throw new NotImplementedException();
    }

    IOGraphGdmElement IOGraphGdmDescriptor.Describe()
    {
        throw new NotImplementedException();
    }
}
