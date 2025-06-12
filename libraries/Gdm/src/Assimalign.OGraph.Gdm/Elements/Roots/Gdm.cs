using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm.Elements;

/// <summary>
/// 
/// </summary>
[DebuggerDisplay("{Name} [Model]")]
public sealed class Gdm : GdmElement, IOGraphGdm
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <exception cref="ArgumentException"></exception>
    public Gdm()
    {
    }

    public GdmGraphCollection Graphs { get; } = new GdmGraphCollection();
    public sealed override GdmElementKind ElementKind { get; } = GdmElementKind.Model;
    IOGraphGdmGraphCollection IOGraphGdm.Graphs => Graphs;
    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => Meta;
    public sealed override IEnumerable<TElement> GetElements<TElement>()
    {
        if (this is TElement element)
        {
            yield return element;
        }

        foreach (var graph in Graphs)
        {
            foreach (var item in graph.GetElements<TElement>())
            {
                yield return item;
            }
        }
    }
}
