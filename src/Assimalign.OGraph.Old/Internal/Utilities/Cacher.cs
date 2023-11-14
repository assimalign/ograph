using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal static class Cacher<TIn, TOut>
    where TIn : notnull
{
    private static readonly ConcurrentDictionary<TIn, TOut> cache;

    static Cacher()
    {
        cache = new();
    }

    public static Func<TIn, TOut> Memoise(Func<TIn, TOut> method)
    {
        return input => cache.TryGetValue(input, out var result) ? 
            result : 
            method(input);
    }
}