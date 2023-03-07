using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal static class SpanExtensions
{
    internal static byte[] GetBytes(string value) => Encoding.UTF8.GetBytes(value);

    internal static ReadOnlySpan<KeyValuePair<FunctionType, byte[]>> Functions => new[]
    {
        new KeyValuePair<FunctionType, byte[]>(FunctionType.Contains, GetBytes("contains")),
        new KeyValuePair<FunctionType, byte[]>(FunctionType.SubString, GetBytes("substring")),

    };

    internal static bool IsFunction(this ReadOnlyMemory<byte> memory, out FunctionType functionType)
    {
        var identifier = memory.ToArray().Select(x => (byte)char.ToLowerInvariant((char)x));

        foreach (var function in Functions)
        {
            if (identifier.SequenceEqual(function.Value))
            {
                functionType = function.Key;
                return true;
            }
        }

        functionType = FunctionType.None;
        return false;
    }   

}
