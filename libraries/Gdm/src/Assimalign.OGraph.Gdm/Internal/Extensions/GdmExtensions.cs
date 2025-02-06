using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal static class GdmExtensions
{
    public static TElement Find<TElement>(this IEnumerable<TElement> elements, Label label) where TElement : IOGraphGdmLabeledElement
    {
        return elements.OfType<TElement>().First(p => p.Equals(label));
    }
}
