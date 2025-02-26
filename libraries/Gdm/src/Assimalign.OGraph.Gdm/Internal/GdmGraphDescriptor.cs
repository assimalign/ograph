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
    private readonly List<Action<GdmGraph>> onTypeAdd;
    private readonly List<Action<GdmGraph>> onVertexAdd;
    private readonly List<Action<GdmGraph>> onEdgeAdd;

    public GdmGraphDescriptor(GdmGraph graph)
    {
        this.graph = graph;
        this.onTypeAdd = new List<Action<GdmGraph>>();
        this.onVertexAdd = new List<Action<GdmGraph>>();
        this.onEdgeAdd = new List<Action<GdmGraph>>();
    }

    public IOGraphGdmGraphDescriptor AddType(IOGraphGdmType type)
    {
        return AddType(graph => type);
    }
    public IOGraphGdmGraphDescriptor AddType(Func<IOGraphGdmGraph, IOGraphGdmType> configure)
    {
        onTypeAdd.Add(graph =>
        {
            var func = ThrowHelper.ThrowIfNull(configure, nameof(configure));
            var type = func.Invoke(graph);


        });

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
    public IOGraphGdmGraphDescriptor AddMeta(string key, string value)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmGraphDescriptor AddEdge(IOGraphGdmEdge edge)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmGraphDescriptor AddEdge(Func<IOGraphGdmGraph, IOGraphGdmEdge> configure)
    {
        throw new NotImplementedException();
    }
}
