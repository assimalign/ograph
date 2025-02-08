using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

using Assimalign.OGraph.Gdm.Elements;

internal class GdmVertexDescriptor : IOGraphGdmVertexDescriptor
{
    private readonly GdmVertex vertex;
    private readonly GdmGraph graph;

    public GdmVertexDescriptor(GdmVertex vertex, GdmGraph graph)
    {
        this.vertex = vertex;
        this.graph = graph;

    }

    public IOGraphGdmVertexDescriptor AddMeta(string key, string value)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexDescriptor HasOperation(IOGraphGdmOperation operation)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexDescriptor HasEntityType(IOGraphGdmEntityType type)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexDescriptor HasEntityType(Func<IOGraphGdmGraph, IOGraphGdmEntityType> type)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmVertexDescriptor HasLabel(Label label)
    {
        throw new NotImplementedException();
    }
}
