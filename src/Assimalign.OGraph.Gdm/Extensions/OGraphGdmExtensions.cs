using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Extensions for <see cref="IOGraphGdm"/>.
/// </summary>
public static class OGraphGdmExtensions
{
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
    /// Returns all the <see cref="IOGraphGdmPrimitiveType"/> instances in the graph model.
    /// </summary>
    /// <param name="model">The graph data model.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<IOGraphGdmPrimitiveType> GetGdmPrimitiveTypes(this IOGraphGdm model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        return model.Elements.OfType<IOGraphGdmPrimitiveType>();
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
}