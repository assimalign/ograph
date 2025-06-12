using System;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

using Internal;

/// <summary>
/// Extensions for <see cref="IOGraphGdm"/>.
/// </summary>
public static class OGraphGdmExtensions
{

    
    /// <summary>
    /// Finds the first element in the collection
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    /// <param name="elements"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public static TElement? Find<TElement>(
        this IEnumerable<TElement> elements, 
        GdmLabel label) 
        where TElement : IOGraphGdmLabeledElement
    {
        AssertNull(elements, nameof(elements));
        return elements.OfType<TElement>().FirstOrDefault(p => p.Equals(label));
    }

    public static TElement? Find<TElement>(
        this IEnumerable<TElement> elements, 
        GdmName name)
        where TElement : IOGraphGdmNamedElement
    {
        AssertNull(elements, nameof(elements));
        return elements.OfType<TElement>().FirstOrDefault(p => p.Equals(name));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static IOGraphGdmGraph GetGraph(this IOGraphGdm model, GdmName name)
    {
        AssertNull(model, nameof(model));
        return model.Graphs.Find<IOGraphGdmGraph>(name);
    }

    private static void AssertNull(object value, string paramName)
    {
        if (value is null)
        {
            ThrowHelper.ThrowArgumentNullException(paramName);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="label"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    //public static bool TryGetType(this IOGraphGdmGraph graph, GdmLabel label, out IOGraphGdmType type)
    //{
    //    throw new NotImplementedException();
    //}
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TGdmBinding"></typeparam>
    /// <param name="element"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    //public static TGdmBinding? GetBinding<TGdmBinding>(this IOGraphGdmBindableElement element, Label label)
    //    where TGdmBinding : IOGraphGdmBinding
    //{
    //    if (element is null)
    //    {
    //        ThrowHelper.ThrowArgumentNullException(nameof(element));
    //    }

    //    return element.Bindings
    //        .OfType<TGdmBinding>()
    //        .FirstOrDefault(p => p.Label.Equals(label));
    //}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="element"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    //public static bool HasBinding(this IOGraphGdmBindableElement element, Label label)
    //{
    //    if (element is null)
    //    {
    //        ThrowHelper.ThrowArgumentNullException(nameof(element));
    //    }
    //    return element.Bindings.Any(p=>p.Label.Equals(label));
    //}
    /// <summary>
    /// Returns all the <see cref="IOGraphGdmNode"/> instances in the graph model.
    /// </summary>
    /// <param name="model">The graph data model.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<IOGraphGdmNode> GetGdmVertices(this IOGraphGdm model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        return model.Graphs.OfType<IOGraphGdmNode>();
    }
    /// <summary>
    /// Returns all the <see cref="IOGraphGdmEdge"/> instances in the graph model.
    /// </summary>
    /// <param name="model">The graph data model.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<IOGraphGdmEdge> GetGdmEdges(this IOGraphGdm model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        return model.Graphs.OfType<IOGraphGdmEdge>();
    }
    /// <summary>
    /// Returns all the <see cref="IOGraphGdmType"/> instances in the graph model.
    /// </summary>
    /// <param name="model">The graph data model.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<IOGraphGdmType> GetGdmTypes(this IOGraphGdm model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        return model.Graphs.OfType<IOGraphGdmType>();
    }
    /// <summary>
    /// Returns all the <see cref="IOGraphGdmScalarType"/> instances in the graph model.
    /// </summary>
    /// <param name="model">The graph data model.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<IOGraphGdmScalarType> GetGdmPrimitiveTypes(this IOGraphGdm model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        return model.Graphs.OfType<IOGraphGdmScalarType>();
    }
    /// <summary>
    /// Returns all the <see cref="IOGraphGdmEnumType"/> instances in the graph model.
    /// </summary>
    /// <param name="model">The graph data model.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<IOGraphGdmEnumType> GetGdmEnumTypes(this IOGraphGdm model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        return model.Graphs.OfType<IOGraphGdmEnumType>();
    }
    /// <summary>
    /// Returns all the <see cref="IOGraphGdmEntityType"/> instances in the graph model.
    /// </summary>
    /// <param name="model">The graph data model.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<IOGraphGdmEntityType> GetGdmEntityTypes(this IOGraphGdm model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        return model.Graphs.OfType<IOGraphGdmEntityType>();
    }
    /// <summary>
    /// Returns all the <see cref="IOGraphGdmComplexType"/> instances in the graph model.
    /// </summary>
    /// <param name="model">The graph data model.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<IOGraphGdmComplexType> GetGdmComplexTypes(this IOGraphGdm model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        return model.Graphs.OfType<IOGraphGdmComplexType>();
    }
    /// <summary>
    /// Returns all the <see cref="IOGraphGdmCollectionType"/> instances in the graph model.
    /// </summary>
    /// <param name="model">The graph data model.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<IOGraphGdmCollectionType> GetGdmCollectionTypes(this IOGraphGdm model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        return model.Graphs.OfType<IOGraphGdmCollectionType>();
    }
    /// <summary>
    /// Returns all the <see cref="IOGraphGdmProperty"/> instances in the graph model.
    /// </summary>
    /// <param name="model">The graph data model.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<IOGraphGdmProperty> GetGdmProperties(this IOGraphGdm model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        return model.Graphs.OfType<IOGraphGdmProperty>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<IOGraphGdmFunction> GetGdmFunctions(this IOGraphGdm model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        return model.Graphs.OfType<IOGraphGdmFunction>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<IOGraphGdmGraph> GetGdmGraphs(this IOGraphGdm model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        return model.Graphs.OfType<IOGraphGdmGraph>();
    }


    //private static IEnumerable<string> GetPaths(this IOGraphGdm model)
    //{
    //    var root = model.Label;

    //    yield return root;

    //    foreach (var vertex in model.GetGdmVertices())
    //    {
    //        foreach (var path in Paths(root, vertex))
    //        {
    //            yield return path;
    //        }
    //    }

    //    IEnumerable<string> Paths(string root, IOGraphGdmVertex vertex)
    //    {
    //        foreach (var edge in vertex.Edges)
    //        {
    //            var key = (vertex.Type as IOGraphGdmEntityType)!.Key.Property.Definition.Label;
    //            var label = string.Join('/', root, $"{key}", edge.Definition.Label);

    //            yield return string.Join('/', root, $"{key}", label);

    //            foreach (var child in Paths(label, edge.Definition.Target.Definition))
    //            {
    //                yield return child;
    //            }
    //        }
    //    }
    //}
}