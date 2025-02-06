using System;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;
using System.Xml.Linq;


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
    public static TElement Find<TElement>(this IEnumerable<TElement> elements, Label label) 
        where TElement : IOGraphGdmLabeledElement
    {
        AssertNull(elements, nameof(elements));
        return elements.OfType<TElement>().First(p => p.Equals(label));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public static IOGraphGdmGraph GetGraph(this IOGraphGdm model, Label label)
    {
        AssertNull(model, nameof(model));
        return model.Elements.Find<IOGraphGdmGraph>(label);
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
    public static bool TryGetType(this IOGraphGdmGraph graph, Label label, out IOGraphGdmType type)
    {
        throw new NotImplementedException();
    }
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
    /// Returns all the <see cref="IOGraphGdmVertex"/> instances in the graph model.
    /// </summary>
    /// <param name="model">The graph data model.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<IOGraphGdmVertex> GetGdmVertices(this IOGraphGdm model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        return model.Elements.OfType<IOGraphGdmVertex>();
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
        return model.Elements.OfType<IOGraphGdmEdge>();
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
        return model.Elements.OfType<IOGraphGdmType>();
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
        return model.Elements.OfType<IOGraphGdmScalarType>();
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
        return model.Elements.OfType<IOGraphGdmEnumType>();
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
        return model.Elements.OfType<IOGraphGdmEntityType>();
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
        return model.Elements.OfType<IOGraphGdmComplexType>();
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
        return model.Elements.OfType<IOGraphGdmCollectionType>();
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
        return model.Elements.OfType<IOGraphGdmProperty>();
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
        return model.Elements.OfType<IOGraphGdmFunction>();
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
        return model.Elements.OfType<IOGraphGdmGraph>();
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