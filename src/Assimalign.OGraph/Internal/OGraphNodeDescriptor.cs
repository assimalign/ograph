using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphNodeDescriptor<T> : IOGraphNodeDescriptor<T>
{






    public IOGraphEdgeDescriptor<TProperty> HasEdge<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        throw new NotImplementedException();
    }

    public IOGraphNodeDescriptor<T> HasKey<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        throw new NotImplementedException();
    }

    public IOGraphNodePropertyDescriptor HasProperty(Name name)
    {
        throw new NotImplementedException();
    }

    public IOGraphNodePropertyDescriptor<TProperty> HasProperty<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        throw new NotImplementedException();
    }

    public IOGraphNodeDescriptor<T> Ignore<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        throw new NotImplementedException();
    }
}
