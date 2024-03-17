using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Internal;

internal static class GdmTypeHelper
{
    private static readonly IDictionary<Type, IOGraphGdmType> cache;

    static GdmTypeHelper()
    {
        cache = new Dictionary<Type, IOGraphGdmType>();
    }


    public static IOGraphGdmType GetType<TType>() where TType : IOGraphGdmType, new()
    {
        IOGraphGdmType obj;
        var type = typeof(TType);

        if (cache.TryGetValue(type, out obj!))
        {
            return obj;
        }

        obj = new TType();

        cache[type] = obj;

        return obj;
    }
}
