using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

using Assimalign.OGraph.Gdm.Elements;

internal class GdmGraphDescriptor : IOGraphGdmGraphDescriptor
{
    private readonly GdmGraph graph;

    public GdmGraphDescriptor(GdmGraph graph)
    {
        this.graph = graph;
    }

    public IOGraphGdmGraphDescriptor AddEdge<TSource, TTarget>(Label label)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmGraphDescriptor AddType(IOGraphGdmType type)
    {
        ThrowHelper.ThrowIfNull(type, nameof(type));

        return AddType(graph =>
        {
            if (graph.TryGetType(type.Label, out var existing))
            {
                return existing;
            }

            return type;
        });
    }
    public IOGraphGdmGraphDescriptor AddType(Func<IOGraphGdmGraph, IOGraphGdmType> configure)
    {
        ThrowHelper.ThrowIfNull(configure, nameof(configure));

        var type = configure.Invoke(graph);

        if (!graph.Types.Contains(type))
        {
            graph.Types.Add(type);
        }

        return this;
    }

    public IOGraphGdmGraphDescriptor AddVertex(IOGraphGdmVertex vertex)
    {
        ThrowHelper.ThrowIfNull(vertex, nameof(vertex));

        return AddVertex(graph =>
        {
            return vertex;
        });
    }

    public IOGraphGdmGraphDescriptor AddVertex(Func<IOGraphGdmGraph, IOGraphGdmVertex> configure)
    {
        throw new NotImplementedException();
    }
}
