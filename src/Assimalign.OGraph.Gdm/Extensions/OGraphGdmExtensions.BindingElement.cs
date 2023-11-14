using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

public static class OGraphGdmBindingElementExtensions
{
    /// <summary>
    /// Checks the element for any bindings matching the type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="element"></param>
    /// <param name="binding"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns></returns>
    public static bool HasBinding<T>(this IOGraphGdmBindingElement element, out T? binding) where T : IOGraphGdmBinding
    {
        binding = default;

        if (element is null)
        {
            GdmThrowHelper.ThrowArgumentNullException(nameof(element));
        }
        foreach (var b in element.Bindings)
        {
            if (b is T match)
            {
                binding = match;
                return true;
            }
        }
        return false;
    }
}
