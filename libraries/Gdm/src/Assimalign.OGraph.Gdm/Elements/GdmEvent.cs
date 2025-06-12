using System;
using System.Collections.Generic;
using System.Text;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public class GdmEvent : GdmBindableElement, IOGraphGdmEvent
{
    public GdmEvent(GdmName name, GdmGraph graph) : base(name)
    {
        Graph = ThrowHelper.ThrowIfNull(graph);
    }
    public GdmType? OutputType { get; }
    public GdmGraph Graph { get; }
    public sealed override GdmElementKind ElementKind { get; } = GdmElementKind.Event;
    IOGraphGdmType? IOGraphGdmEvent.OutputType => OutputType;
    IOGraphGdmGraph IOGraphGdmEvent.Graph => Graph;
}
