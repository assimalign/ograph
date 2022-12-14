using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal static class EnumerableExtensions
{
    internal static bool OneOf<T>(this IEnumerable<T> enumerable, params T[] values)
        where T : IEquatable<T>
    {

        foreach (var item in enumerable)
        {
            foreach(var value in values)
            {
                if (value.Equals(item))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
