using System;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public abstract class GdmEdge : GdmLabeledElement, IOGraphGdmEdge
{
    public GdmEdge(GdmLabel label, GdmVertex target, GdmVertex source, GdmGraph graph)
    {
        Label = ThrowHelper.ThrowIfLabelEmpty(label);
        Target = ThrowHelper.ThrowIfNull(target);
        Source = ThrowHelper.ThrowIfNull(source);
        Graph = ThrowHelper.ThrowIfNull(graph);
    }

    public virtual GdmVertex Target { get; }
    public virtual GdmVertex Source { get;  }
    public GdmGraph Graph { get; }
    public sealed override GdmLabel Label { get; }
    public sealed override GdmElementKind ElementKind { get; } = GdmElementKind.Edge;
    IOGraphGdmVertex IOGraphGdmEdge.Target => Target;
    IOGraphGdmVertex IOGraphGdmEdge.Source => Source;
    IOGraphGdmStepCollection IOGraphGdmEdge.Steps => throw new NotImplementedException();
    IOGraphGdmGraph IOGraphGdmEdge.Graph => Graph;
}
