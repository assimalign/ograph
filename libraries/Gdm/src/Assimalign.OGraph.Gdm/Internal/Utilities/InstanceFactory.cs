using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal static class InstanceFactory
{
    private static ConcurrentDictionary<Type, InstancePool> pools = new();
    
    public static object? Create(Type type)
    {
        var pool = pools.GetOrAdd(type, new InstancePool());

        var value = pool.Rent();

        return value.Value;
    }
}