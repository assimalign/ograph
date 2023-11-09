using Assimalign.OGraph.Gdm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphVertexBindingDescriptor<T>
{
    IOGraphVertexPropertyBindingDescriptor<T> Property<TMember>(Expression<Func<T, TMember>> expression);
}


public interface IOGraphVertexPropertyBindingDescriptor<T>
{
    IOGraphVertexPropertyBindingDescriptor<T> UseResolver(OGraphPropertyResolver resolver);
}

public abstract class OGraphVertexBinding<T> : GdmVertex<T> where T : class, new()
{

    protected virtual void Configure(IOGraphVertexBindingDescriptor<T> descriptor)
    {
        
    }
}



public abstract class OGraphPropertyBinding
{

}