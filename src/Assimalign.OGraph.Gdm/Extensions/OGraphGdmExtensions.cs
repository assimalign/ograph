using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

public static class OGraphGdmExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
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
    /// 
    /// </summary>
    /// <param name="model"></param>
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
    /// 
    /// </summary>
    /// <param name="model"></param>
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
    /// 
    /// </summary>
    /// <param name="model"></param>
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
    /// 
    /// </summary>
    /// <param name="model"></param>
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
    /// 
    /// </summary>
    /// <param name="model"></param>
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
    /// 
    /// </summary>
    /// <param name="model"></param>
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
    /// 
    /// </summary>
    /// <param name="model"></param>
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
}
