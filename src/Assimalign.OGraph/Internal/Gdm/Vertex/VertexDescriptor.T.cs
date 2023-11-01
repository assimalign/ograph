using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assimalign.OGraph.Internal;

internal class VertexDescriptor<T> : IOGraphVertexDescriptor<T> where T : class, new()
{
    private readonly OGraphVertex<T> vertex;

    public VertexDescriptor(OGraphVertex<T> vertex)
    {
        this.vertex = vertex;
    }
    public IOGraphVertexEdgeDescriptor HasEdge(Label label)
    {
        throw new NotImplementedException();
    }

    public IOGraphVertexDescriptor<T> HasKey(Label key)
    {
        throw new NotImplementedException();
    }

    public IOGraphVertexDescriptor<T> HasKey<TMember>(Expression<Func<T, TMember>> expression)
    {
        throw new NotImplementedException();
    }

    public IOGraphVertexDescriptor<T> HasLabel(Label label)
    {
        if (!vertex.labels.Contains(label))
        {
            var index = vertex.labels.Length + 1;
            Array.Resize(ref vertex.labels, index);
            vertex.labels[index] = label;
        }
        return this;
    }
    public IOGraphVertexDescriptor<T> HasMetadata(string key, object value)
    {
        vertex.metadata.Add(key, value);
        return this;
    }
    public IOGraphPropertyDescriptor HasProperty(Label name)
    {
        throw new NotImplementedException();
    }

    public IOGraphPropertyDescriptor<TMember> HasProperty<TMember>(Expression<Func<T, TMember>> expression)
    {
        throw new NotImplementedException();
    }

    public IOGraphVertexQueryOperationDescriptor HasQuery(Label label)
    {
        throw new NotImplementedException();
    }

    public IOGraphVertexDescriptor<T> Ignore(Label name)
    {
        throw new NotImplementedException();
    }

    public IOGraphVertexDescriptor<T> Ignore<TMember>(System.Linq.Expressions.Expression<Func<T, TMember>> expression)
    {
        throw new NotImplementedException();
    }
}
