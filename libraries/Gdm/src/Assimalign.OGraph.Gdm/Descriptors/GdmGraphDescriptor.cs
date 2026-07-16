using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Assimalign.OGraph.Gdm;

using Elements;
using Internal;

public sealed class GdmGraphDescriptor : IOGraphGdmGraphDescriptor
{
    private readonly GdmGraph _graph;

    private readonly List<Action<GdmGraph>> _types;
    private readonly List<Action<GdmGraph>> _vertices;
    private readonly List<Action<GdmGraph>> _edges;
    private readonly List<Action<GdmGraph>> _after;

    internal GdmGraphDescriptor(GdmGraph graph)
    {
        _graph = graph;
        _types = new List<Action<GdmGraph>>();
        _vertices = new List<Action<GdmGraph>>();
        _edges = new List<Action<GdmGraph>>();
        _after = new List<Action<GdmGraph>>();
    }

    #region Methods - Type Registration

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="func"></param>
    /// <returns></returns>
    public GdmGraphDescriptor AddType<T>(Func<GdmGraph, T> func) where T : GdmType
    {
        ThrowHelper.ThrowIfNull(func);

        _types.Add(graph =>
        {
            var type = func.Invoke(graph);

            type.Configure();

            graph.Types.Add(type);
        });

        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GdmGraphDescriptor AddType(GdmType type)
    {
        ThrowHelper.ThrowIfNull(type);

        return AddType(graph =>
        {
            type.SetGraph(graph);
            return type;
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public GdmGraphDescriptor AddScalarType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>()
        where T : GdmScalarType
    {
        return AddType(graph =>
        {
            if (Activator.CreateInstance(typeof(T), graph) is not GdmScalarType scalarType)
            {
                throw new ArgumentException("");
            }

            return scalarType;
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public GdmGraphDescriptor AddEnumType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>()
        where T : GdmEnumType
    {
        return AddType(graph =>
        {
            if (Activator.CreateInstance(typeof(T), graph) is not GdmEnumType enumType)
            {
                throw new ArgumentException("");
            }

            return enumType;
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public GdmGraphDescriptor AddComplexType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>()
        where T : GdmComplexType
    {
        return AddType(graph =>
        {
            if (Activator.CreateInstance(typeof(T), graph) is not GdmComplexType complexType)
            {
                throw new ArgumentException("");
            }

            return complexType;
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="configure"></param>
    /// <returns></returns>
    public GdmGraphDescriptor AddComplexType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(Action<GdmComplexTypeDescriptor<T>> configure)
    {
        return AddType(graph => new GdmComplexTypeDefault<T>(graph, ThrowHelper.ThrowIfNull(configure)));
    }


    public GdmGraphDescriptor AddCollectionType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>()
        where T : GdmCollectionType
    {
        // TODO: [O01.01.02.02] type-system runtime
        throw new NotImplementedException();
    }

    public GdmGraphDescriptor AddCollectionType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(GdmName itemTypeName)
        where T : GdmCollectionType
    {
        // TODO: [O01.01.02.02] type-system runtime
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    //[UnconditionalSuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code", Justification = "<Pending>")]
    //    public GdmGraphDescriptor AddEntityType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>()
    //        where T : class, new()
    //    {
    //        var assembly = Assembly.GetAssembly(typeof(T));

    //        if (assembly is null)
    //        {
    //            return this;
    //        }

    //#pragma warning disable IL2026 // Dereference of a possibly null reference.
    //        foreach (var type in assembly.GetTypes())
    //        {
    //            foreach (var attribute in type.GetCustomAttributes())
    //            {

    //            }
    //        }
    //#pragma warning restore CS8602 // Dereference of a possibly null reference.

    //        return this;
    //    }


    public GdmGraphDescriptor AddEntityType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(Action<GdmEntityTypeDescriptor<T>> configure)
    {
        return AddType(graph => new GdmEntityTypeDefault<T>(graph, ThrowHelper.ThrowIfNull(configure)));
    }


    #endregion

    #region Methods - Nodes


    //public GdmGraphDescriptor AddVertex<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(Func<GdmGraph, GdmVertex<T>> configure)
    //    where T : class, new()
    //{
    //    GdmVertex<T>.Create(_graph, )
    //    return this;
    //}

    public GdmGraphDescriptor AddNode(Func<GdmGraph, GdmNode> func)
    {
        ThrowHelper.ThrowIfNull(func);

        _vertices.Add(graph =>
        {
            var vertex = ThrowHelper.ThrowIfNull(func.Invoke(graph));

            vertex.Configure();

            graph.Vertices.Add(vertex);
        });

        return this;
    }

    public GdmGraphDescriptor AddNode(GdmNode vertex)
    {


        return this;
    }
    public GdmGraphDescriptor AddNode<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>() 
        where T : GdmNode
    {
        return AddNode(graph =>
        {
            if (Activator.CreateInstance(typeof(T), graph) is not GdmNode vertex)
            {
                throw new ArgumentException("");
            }

            return vertex;
        });

    }
    public GdmGraphDescriptor AddNode<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(Action<GdmVertexDescriptor<T>> configure)
        where T : class, new()
    {
        return AddNode(graph =>
        {
            return new GdmNodeDefault<T>(graph, configure);
        });
    }
    public GdmGraphDescriptor AddNode<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(GdmLabel label, Action<GdmEntityTypeDescriptor<T>> configure)
        where T : class, new()
    {
        AddType(graph =>
        {
            return new GdmEntityTypeDefault<T>(graph, configure);
        });

        return AddNode(graph =>
        {
            var entityType = graph.Types
                .OfType<GdmEntityType>()
                .FirstOrDefault(type => type.RuntimeType == typeof(T));

            if (entityType is null)
            {
                throw new Exception();
            }

            return new GdmNode(label.Value, entityType, graph);
        });
    }

    #endregion


    public GdmGraphDescriptor AddEdge(GdmEdge edge)
    {

        return this;
    }

    private void OnBuild(List<Action<GdmGraph>> actions)
    {
        foreach (var action in actions)
        {
            action.Invoke(_graph);
        }
    }

    IOGraphGdmGraphDescriptor IOGraphGdmGraphDescriptor.AddEdge(IOGraphGdmEdge edge)
    {
        return AddEdge(ThrowHelper.ThrowIfNotType<GdmEdge>(edge));
    }
    IOGraphGdmGraphDescriptor IOGraphGdmGraphDescriptor.AddMeta(string key, string value)
    {
        throw new NotImplementedException();
    }
    IOGraphGdmGraphDescriptor IOGraphGdmGraphDescriptor.AddType(IOGraphGdmType type)
    {
        return AddType(ThrowHelper.ThrowIfNotType<GdmType>(type));
    }
    IOGraphGdmGraphDescriptor IOGraphGdmGraphDescriptor.AddNode(IOGraphGdmNode vertex)
    {
        return AddNode(ThrowHelper.ThrowIfNotType<GdmNode>(vertex));
    }

    internal GdmGraph Describe()
    {
        OnBuild(_types);
        OnBuild(_vertices);
        OnBuild(_edges);


        return _graph;
    }

    IOGraphGdmGraphDescriptor IOGraphGdmGraphDescriptor.AddQuery(IOGraphGdmOperation operation)
    {
        throw new NotImplementedException();
    }

    IOGraphGdmGraphDescriptor IOGraphGdmGraphDescriptor.AddCommand(IOGraphGdmOperation operation)
    {
        throw new NotImplementedException();
    }

    IOGraphGdmGraphDescriptor IOGraphGdmGraphDescriptor.AddEvent(IOGraphGdmOperation operation)
    {
        throw new NotImplementedException();
    }

    IOGraphGdmGraphDescriptor IOGraphGdmGraphDescriptor.AddSubscriber(IOGraphGdmSubscriber subscriber)
    {
        throw new NotImplementedException();
    }



    //public IOGraphGdmGraphDescriptor AddType(IOGraphGdmType type)
    //{
    //    return AddType(graph => type);
    //}
    //public IOGraphGdmGraphDescriptor AddType(Func<IOGraphGdmGraph, IOGraphGdmType> configure)
    //{
    //    _onTypeAdd.Add(graph =>
    //    {
    //        var func = ThrowHelper.ThrowIfNull(configure, nameof(configure));
    //        var type = func.Invoke(graph);


    //    });

    //    return this;
    //}
    //public IOGraphGdmGraphDescriptor AddVertex(IOGraphGdmVertex vertex)
    //{
    //    ThrowHelper.ThrowIfNull(vertex, nameof(vertex));

    //    return AddVertex(graph =>
    //    {
    //        return vertex;
    //    });
    //}
    //public IOGraphGdmGraphDescriptor AddVertex(Func<IOGraphGdmGraph, IOGraphGdmVertex> configure)
    //{
    //    throw new NotImplementedException();
    //}
    //public IOGraphGdmGraphDescriptor AddMeta(string key, string value)
    //{
    //    throw new NotImplementedException();
    //}

    //public IOGraphGdmGraphDescriptor AddEdge(IOGraphGdmEdge edge)
    //{
    //    throw new NotImplementedException();
    //}

    //public IOGraphGdmGraphDescriptor AddEdge(Func<IOGraphGdmGraph, IOGraphGdmEdge> configure)
    //{
    //    throw new NotImplementedException();
    //}

    //public IOGraphGdmGraph Build()
    //{
    //    throw new NotImplementedException();
    //}
}
