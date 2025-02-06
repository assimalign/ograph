using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

using Internal;
using Elements;

public static class OGraphGdmVertexExtensions
{
    /// <summary>
    /// Tries to retrieve a property with the provided <paramref name="label"/>.
    /// </summary>
    /// <param name="vertex"></param>
    /// <param name="label"></param>
    /// <param name="property"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="GdmException"></exception>
    public static bool TryGetProperty(this IOGraphGdmVertex vertex, Label label, out IOGraphGdmProperty? property)
    {
        property = null;

        if (vertex is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(vertex));
        }
        if (vertex.Type is not IOGraphGdmEntityType entity)
        {
            ThrowHelper.ThrowVertexInvalidTypeReferenceIsNotEntityType(vertex.Label);
        }
        else
        {
            property = entity.Members.OfType<IOGraphGdmProperty>().FirstOrDefault(p => p.Label == label);
        }

        return property is not null;
    }
    
    /// <summary>
    /// Get's the properties from the entity reference bound to the vertex.
    /// </summary>
    /// <param name="vertex"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="GdmException"></exception>
    public static IEnumerable<IOGraphGdmProperty> GetProperties(this IOGraphGdmVertex vertex)
    {
        if (vertex is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(vertex));
        }
        if (vertex.Type is not IOGraphGdmEntityType entity)
        {
            ThrowHelper.ThrowVertexInvalidTypeReferenceIsNotEntityType(vertex.Label);
        }

        return (vertex.Type as IOGraphGdmEntityType)!.Members.OfType<IOGraphGdmProperty>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vertex"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="GdmException"></exception>
    public static IOGraphGdmEntityType GetGdmEntityType(this IOGraphGdmVertex vertex)
    {
        IOGraphGdmEntityType type = null!;

        if (vertex is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(vertex));
        }
        else if (vertex.Type is null || vertex.Type is null)
        {
            ThrowHelper.ThrowVertexInvalidTypeReferenceIsNull(vertex.Label);
        }
        else if (vertex.Type is not IOGraphGdmEntityType entity)
        {
            ThrowHelper.ThrowVertexInvalidTypeReferenceIsNotEntityType(vertex.Label);
        }
        else
        {
            type = entity;
        }
        
        return type;
    }

    /// <summary>
    /// Checks if the vertex entity type is bound to the runtime type.
    /// </summary>
    /// <param name="vertex"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="GdmException"></exception>
    public static bool IsRuntimeTypeMatch(this IOGraphGdmVertex vertex, Type? type)
    {
        if (type is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(type));
        }

        var entityType = vertex.GetGdmEntityType();

        return entityType.RuntimeType!.IsAssignableFrom(type);
    }
}