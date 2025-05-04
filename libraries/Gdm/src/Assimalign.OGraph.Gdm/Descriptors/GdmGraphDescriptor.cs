using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm;

using Elements;
using Internal;
using System.Reflection;

public class GdmGraphDescriptor : IOGraphGdmGraphDescriptor
{
    private readonly GdmGraph _graph;
    private readonly List<Action<GdmGraph>> _scalarTypes;
    private readonly List<Action<GdmGraph>> _complexTypes;
    private readonly List<Action<GdmGraph>> _entityTypes;
    private readonly List<Action<GdmGraph>> _collectionTypes;
    private readonly List<Action<GdmGraph>> _vertices;
    private readonly List<Action<GdmGraph>> _edges;
    private readonly List<Action<GdmGraph>> _after;

    public GdmGraphDescriptor(GdmGraph graph)
    {
        _graph = graph;
        _scalarTypes = new List<Action<GdmGraph>>();
        _complexTypes = new List<Action<GdmGraph>>();
        _entityTypes = new List<Action<GdmGraph>>();
        _collectionTypes = new List<Action<GdmGraph>>();
        _vertices = new List<Action<GdmGraph>>();
        _edges = new List<Action<GdmGraph>>();
        _after = new List<Action<GdmGraph>>();
    }

    #region Methods - Type Registration

    //public GdmGraphDescriptor ConvertAllNamesToCamalCase(params GdmElementKind[] elements)
    //{
    //    _after.Add(graph =>
    //    {
    //        if (elements.Contains(GdmElementKind.Member))
    //        {
    //            foreach (var member in graph.GetElementsOfType<GdmMember>())
    //            {
    //                member.Name = member.Name.ToCamalCase();
    //            }
    //        }
    //    });

    //    return this;
    //}


    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GdmGraphDescriptor AddType(GdmType type)
    {
        ThrowHelper.ThrowIfNull(type);

        if (type is IOGraphGdmScalarType scalarType)
        {
            _scalarTypes.Add(graph =>
            {
                type.Initialize(graph);
            });
            return this;
        }
        if (type is IOGraphGdmEnumType enumType)
        {

            return this;
        }
        if (type is IOGraphGdmEntityType entityType)
        {
            _entityTypes.Add(graph =>
            {

            });
            return this;
        }
        if (type is IOGraphGdmComplexType complexType)
        {


            return this;
        }

        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public GdmGraphDescriptor AddType<T>(Func<GdmGraph, T> func) where T : GdmType
    {
        var typeInfo = typeof(T);

        _scalarTypes.Add(graph =>
        {
            var gdmType = ThrowHelper.ThrowIfNull(func).Invoke(graph);

            if (gdmType is null)
            {
                ThrowHelper.ThrowArgumentException("");
            }

            gdmType.Initialize(graph);
        });

        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public GdmGraphDescriptor AddType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>() where T : GdmType, new()
    {
        var typeInfo = typeof(T);

        if (typeInfo.IsAssignableTo(typeof(IOGraphGdmEntityType)))
        {

            return this;
        }
        else if (typeInfo.IsAssignableTo(typeof(IOGraphGdmComplexType)))
        {
            _complexTypes.Add(graph =>
            {
                var complexType = new T();

                complexType.Initialize(graph);

                // var descriptor = new GdmComplexTypeDescriptor<>
            });
        }
        else if (typeInfo.IsAssignableTo(typeof(IOGraphGdmScalarType)))
        {
            _scalarTypes.Add(graph =>
            {
                var scalarType = new T();

                graph.Types.Add(scalarType);
            });
        }

        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public GdmGraphDescriptor AddComplexType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>()
        where T : class, new()
    {


        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="configure"></param>
    /// <returns></returns>
    public GdmGraphDescriptor AddComplexType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(Action<GdmComplexTypeDescriptor<T>> configure)
        where T : class, new()
    {


        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    //[UnconditionalSuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code", Justification = "<Pending>")]
    public GdmGraphDescriptor AddEntityType<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>()
        where T : class, new()
    {
        var assembly = Assembly.GetAssembly(typeof(T));

        if (assembly is null)
        {
            return this;
        }

#pragma warning disable IL2026 // Dereference of a possibly null reference.
        foreach (var type in assembly.GetTypes())
        {
            foreach (var attribute in type.GetCustomAttributes())
            {

            }
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return this;
    }


    public GdmGraphDescriptor AddEntityType<T>(Action<GdmEntityTypeDescriptor<T>> configure)
        where T : class, new()
    {


        return this;
    }


    #endregion

    #region Methods - Vertices


    //public GdmGraphDescriptor AddVertex<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(Func<GdmGraph, GdmVertex<T>> configure)
    //    where T : class, new()
    //{
    //    GdmVertex<T>.Create(_graph, )
    //    return this;
    //}

    public GdmGraphDescriptor AddVertex(GdmVertex vertex)
    {


        return this;
    }
    public GdmGraphDescriptor AddVertex<T>() where T : GdmVertex, new()
    {

        return this;
    }
    public GdmGraphDescriptor AddVertex<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(Action<GdmVertexDescriptor<T>> configure)
        where T : class, new()
    {
        ThrowHelper.ThrowIfNull(configure);

        var descriptor = new GdmVertexDescriptor<T>(this);

        configure.Invoke(descriptor);

        _vertices.Add(graph =>
        {
            var vertex = descriptor.GetVertex();

            graph.Vertices.Add(vertex);
        });

        //_vertices.Add(graph =>
        //{

        //});
        //var vertex = GdmVertex<T>.Create(configure);

        //vertex.Graph = _graph;

        //_graph.Vertices.Add(vertex);


        return this;
    }
    public GdmGraphDescriptor AddVertex<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] T>(GdmLabel label, Action<GdmEntityTypeDescriptor<T>> configure)
        where T : class, new()
    {

        return this;
    }

    #endregion

    public GdmGraph Build()
    {
        OnBuild(_scalarTypes);
        OnBuild(_complexTypes);
        OnBuild(_entityTypes);
        OnBuild(_collectionTypes);
        OnBuild(_vertices);
        OnBuild(_edges);

        // Lock all collections




        return _graph;
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
        throw new NotImplementedException();
    }

    IOGraphGdmGraphDescriptor IOGraphGdmGraphDescriptor.AddMeta(string key, string value)
    {
        throw new NotImplementedException();
    }

    IOGraphGdmGraphDescriptor IOGraphGdmGraphDescriptor.AddType(IOGraphGdmType type)
    {
        return AddType(ThrowHelper.ThrowIfNotType<GdmType>(type));
    }

    IOGraphGdmGraphDescriptor IOGraphGdmGraphDescriptor.AddVertex(IOGraphGdmVertex vertex)
    {
        throw new NotImplementedException();
    }

    IOGraphGdmGraph IOGraphGdmDescriptor<IOGraphGdmGraph>.Describe()
    {
        throw new NotImplementedException();
    }

    IOGraphGdmElement IOGraphGdmDescriptor.Describe()
    {
        return (this as IOGraphGdmDescriptor).Describe();
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
