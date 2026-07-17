using System;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public abstract class GdmEdge : GdmBindableElement, IOGraphGdmEdge
{
    public GdmEdge(GdmName name, GdmNode target, GdmNode source, GdmGraph graph) : base(name)
    {
        Target = ThrowHelper.ThrowIfNull(target);
        Source = ThrowHelper.ThrowIfNull(source);
        Graph = ThrowHelper.ThrowIfNull(graph);
    }

    public GdmNode Target { get; }
    public GdmNode Source { get;  }
    public GdmGraph Graph { get; }
    public sealed override GdmElementKind ElementKind { get; } = GdmElementKind.Edge;
    IOGraphGdmNode IOGraphGdmEdge.Target => Target;
    IOGraphGdmNode IOGraphGdmEdge.Source => Source;
    IOGraphGdmGraph IOGraphGdmEdge.Graph => Graph;
}
