using System;
using System.Linq;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

using Internal;

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
    /// <exception cref="OGraphGdmException"></exception>
    public static bool TryGetProperty(this IOGraphGdmVertex vertex, Label label, out IOGraphGdmProperty? property)
    {
        property = null;

        if (vertex is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(vertex));
        }
        if (vertex.Type is null || vertex.Type.Definition is null)
        {
            GdmThrowHelper.ThrowVertexInvalidTypeReferenceIsNull(vertex.Label);
        }
        if (vertex.Type.Definition is not IOGraphGdmEntityType entity)
        {
            GdmThrowHelper.ThrowVertexInvalidTypeReferenceIsNotEntityType(vertex.Label);
        }
        else
        {
            property = entity.Properties.FirstOrDefault(p => p.Label == label);
        }

        return property is not null;
    }
    /// <summary>
    /// Get's the properties from the entity reference bound to the vertex.
    /// </summary>
    /// <param name="vertex"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="OGraphGdmException"></exception>
    public static IEnumerable<IOGraphGdmProperty> GetProperties(this IOGraphGdmVertex vertex)
    {
        if (vertex is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(vertex));
        }
        if (vertex.Type is null || vertex.Type.Definition is null)
        {
            GdmThrowHelper.ThrowVertexInvalidTypeReferenceIsNull(vertex.Label);
        }
        if (vertex.Type.Definition is not IOGraphGdmEntityType entity)
        {
            GdmThrowHelper.ThrowVertexInvalidTypeReferenceIsNotEntityType(vertex.Label);
        }
        else 
        {
            foreach (var property in entity.Properties)
            {
                yield return property;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vertex"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="OGraphGdmException"></exception>
    public static IOGraphGdmEntityType GetGdmEntityType(this IOGraphGdmVertex vertex)
    {
        IOGraphGdmEntityType type = null!;

        if (vertex is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(vertex));
        }
        else if (vertex.Type is null || vertex.Type.Definition is null)
        {
            GdmThrowHelper.ThrowVertexInvalidTypeReferenceIsNull(vertex.Label);
        }
        else if (vertex.Type.Definition is not IOGraphGdmEntityType entity)
        {
            GdmThrowHelper.ThrowVertexInvalidTypeReferenceIsNotEntityType(vertex.Label);
        }
        else
        {
            type = entity;
        }
        
        return type;
    }
}
