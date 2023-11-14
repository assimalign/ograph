using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphModelVertexDescriptor<T>
    where T : class, new()
{
    IOGraphModelVertexDescriptor<T> HasLabel(Label label);
    IOGraphModelVertexDescriptor<T> HasIdentifier<TMember>(Expression<Func<T, TMember>> expression);
    IOGraphModelVertexDescriptor<T> HasProperty(Label propertyName);
    IOGraphModelVertexDescriptor<T> HasProperty<TMember>(Expression<Func<T, TMember>> expression);
    IOGraphModelVertexDescriptor<T> HasProperty<TMember>(Expression<Func<T, TMember>> expression, Action<IOGraphComplexTypeDescriptor<TMember>>)
        where TMember : class;

    IOGraphModelEdgeDescriptor HasEdge(Label label);
}