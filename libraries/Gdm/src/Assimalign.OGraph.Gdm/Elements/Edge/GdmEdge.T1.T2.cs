using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm.Elements;

public class GdmEdge<
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TTarget,
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TSource> : IOGraphGdmEdge
    where TTarget : class, new()
    where TSource : class, new()
{

    public GdmEdge(GdmLabel label, GdmVertex<TTarget> target, GdmVertex<TSource> source, GdmGraph graph)
    {
        Label = label;
        Target = target;
        Source = source;
        Graph = graph;
    }

    public GdmLabel Label { get; internal set; }
    public GdmVertex<TTarget> Target { get; internal set; }
    public GdmVertex<TSource> Source { get; internal set; }
    public GdmGraph Graph { get; internal set; }
    public GdmMetadata Meta { get; } = new GdmMetadata();
    public GdmElementKind ElementKind => GdmElementKind.Edge;

    IOGraphGdmVertex IOGraphGdmEdge.Target => Target;
    IOGraphGdmVertex IOGraphGdmEdge.Source => Source;
    IOGraphGdmGraph IOGraphGdmEdge.Graph => Graph;
    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => Meta;

    IOGraphGdmStepCollection IOGraphGdmEdge.Steps => throw new NotImplementedException();

    GdmLabel IOGraphGdmLabeledElement.Label => throw new NotImplementedException();

    GdmElementKind IOGraphGdmElement.ElementKind => throw new NotImplementedException();
}
