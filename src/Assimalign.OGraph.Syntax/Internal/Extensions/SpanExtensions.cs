using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal static class SpanExtensions
{
    // This will cache the result of the specific memory. This will prevent constant re-checking of the same memory
    private static ConcurrentDictionary<ReadOnlyMemory<byte>, (bool, FunctionType)> cache = new();

    internal static byte[] GetBytes(string value) => Encoding.UTF8.GetBytes(value);

    internal static ReadOnlySpan<KeyValuePair<FunctionType, byte[]>> Functions => new[]
    {
        new KeyValuePair<FunctionType, byte[]>(FunctionType.Contains, GetBytes("contains")),
        new KeyValuePair<FunctionType, byte[]>(FunctionType.SubString, GetBytes("substring")),
        new KeyValuePair<FunctionType, byte[]>(FunctionType.StartsWith, GetBytes("startswith")),

    };


    internal static bool IsFunction(this ReadOnlyMemory<byte> memory) => IsFunction(memory, out var functionType);
    internal static bool IsFunction(this ReadOnlyMemory<byte> memory, out FunctionType functionType)
    {
        var result = cache.GetOrAdd(memory, mem =>
        {
            var identifier = mem.ToArray().Select(x => (byte)char.ToLowerInvariant((char)x));

            foreach (var function in Functions)
            {
                if (identifier.SequenceEqual(function.Value))
                {
                    return (true, function.Key);
                }
            }

            return (false, default(FunctionType));

        });


        functionType = result.Item2;

        return result.Item1;
    }   
}
